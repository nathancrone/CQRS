using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Amazon;
using Amazon.SimpleWorkflow;
using Amazon.SimpleWorkflow.Model;

using CQRS.AWS.DecisionConsole.Shared;

using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Repository;

namespace CQRS.AWS.DecisionConsole
{
    public class DecisionWorker
    {
        IAmazonSimpleWorkflow _swfClient = new AmazonSimpleWorkflowClient();
        System.Threading.Tasks.Task _task;
        CancellationToken _cancellationToken;
        private WFTracking _tracker;
        //VirtualConsole _console;

        public DecisionWorker()
        {
            _tracker = new WFTracking();
            //this._console = console;
        }

        /// <summary>
        /// Kick off the worker to poll and process decision tasks
        /// </summary>
        /// <param name="cancellationToken"></param>
        public void Start(CancellationToken cancellationToken = default(CancellationToken))
        {
            this._cancellationToken = cancellationToken;
            this._task = System.Threading.Tasks.Task.Run((System.Action)this.PollAndDecide);
        }

        /// <summary>
        /// Polls for descision tasks and decides what decisions to make.
        /// </summary>
        void PollAndDecide()
        {
            Console.WriteLine("Minimal Workflow Started");
            while (!_cancellationToken.IsCancellationRequested)
            {
                DecisionTask task = Poll();
                if (!string.IsNullOrEmpty(task.TaskToken))
                {
                    //Create the next set of decision based on the current state and 
                    //the execution history
                    List<Decision> decisions = Decide(task);

                    //Complete the task with the new set of decisions
                    CompleteTask(task.TaskToken, decisions);
                }
                //Sleep to avoid aggressive polling
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Helper method to poll for decision task from the task list.
        /// </summary>
        /// <returns>Decision task returned from the long poll</returns>
        DecisionTask Poll()
        {
            Console.WriteLine("Polling for decision task ...");
            PollForDecisionTaskRequest request = new PollForDecisionTaskRequest()
            {
                Domain = Constants.WFDomain,
                TaskList = new TaskList()
                {
                    Name = Constants.WFTaskList
                }
            };
            PollForDecisionTaskResponse response = _swfClient.PollForDecisionTask(request);
            return response.DecisionTask;
        }

        /// <summary>
        /// Looks at the events on the task to find all the completed activities. Using the list of completed activites it figures out
        ///  what thumbnail hasn't been created yet and create a decision to start an activity task for that image size.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        List<Decision> Decide(DecisionTask task)
        {
            Console.WriteLine("Processing decision task ...");
            List<Decision> decisions = new List<Decision>();
            
            List<ActivityTaskCompletedResult> activityStates;
            WorkflowExecutionStartedInput startingInput;
            ProcessHistory(task, out startingInput, out activityStates);

            //get the current request...
            Request RequestCurrent = _tracker.GetRequestWithActions(startingInput.RequestId);

            //if the request has no current state. initialize the request...
            if (RequestCurrent.CurrentStateId == null)
            {
                _tracker.RequestInitialize(startingInput.RequestId);

                //the request should have a new state and actions...
                RequestCurrent = _tracker.GetRequestWithActions(startingInput.RequestId);
                
                //code to tell SWF to fire activity for the new state
                decisions.Add(CreateActivityDecision(startingInput, RequestCurrent.RequestActions.Where(x => x.IsActive && !x.IsComplete).FirstOrDefault().RequestActionId ?? 0));
            }

            //compare the "active" request actions that are not "complete" against the history
            //this line finds the "intersection" of those two lists
            //(need to filter this list down to only Simple Workflow actions. right now its working against all active actions)
            List<ActivityTaskCompletedResult> results = activityStates.Join(RequestCurrent.RequestActions.Where(x => x.IsActive && !x.IsComplete).ToList(), a => a.RequestActionId, b => b.RequestActionId, (c, d) => c).ToList();

            //mark any "active" but not "complete" request actions as "complete"
            foreach (ActivityTaskCompletedResult result in results)
            {
                _tracker.RequestActionComplete(result.RequestActionId);
            }

            
            //check whether the request's actions have reached a point where a transition can be followed...
            Transition Transition = _tracker.GetCompletedTransitionForRequest(startingInput.RequestId);

            //if a transition can be followed...
            if (Transition != null)
            {
                //follow the transition
                _tracker.RequestFollowTransition(startingInput.RequestId, Transition.TransitionId);

                //the request should have a new state and actions...
                RequestCurrent = _tracker.GetRequestWithActions(startingInput.RequestId);
                
                RequestAction ActiveNotComplete = RequestCurrent.RequestActions.Where(x => x.IsActive && !x.IsComplete).FirstOrDefault();
                
                if (ActiveNotComplete != null)
                {
                    //code to tell SWF to fire activity for the new state
                    decisions.Add(CreateActivityDecision(startingInput, ActiveNotComplete.RequestActionId ?? 0));
                }
                else
                {
                    Console.WriteLine("Workflow execution complete for " + startingInput.RequestId);
                    //code to tell SWF to complete workflow execution
                    decisions.Add(CreateCompleteWorkflowExecutionDecision(activityStates));
                }

            }
            

            //State StateCurrent = _tracker.GetState(RequestCurrent.CurrentStateId ?? 0);

            //if (StateCurrent.TransitionsFrom.Count() > 0) 
            //{

            //}
            //else //if there are no transitions from current state to another state...
            //{
            //    Console.WriteLine("Workflow execution complete for " + startingInput.RequestId);
            //    decisions.Add(CreateCompleteWorkflowExecutionDecision(activityStates));
            //}


            //// Loop through all the diffrent image sizes the workflow will create and
            //// when we find one missing create that activity task for the missing image.
            ////
            //// To keep the sample simple each activity is scheduled one at a time. For better performance
            //// the activities could be scheduled in parallel and then use the decider to check if all the activities 
            //// have been completed.
            //for (int size = 256; size >= 16; size /= 2)
            //{
            //    if (activityStates.FirstOrDefault(x => x.ImageSize == size) == null)
            //    {
            //        decisions.Add(CreateActivityDecision(startingInput, size));
            //        break;
            //    }
            //}

            //// If there were no decisions that means all the thumbnails have been created so we decided the workflow execution is complete.
            //if (decisions.Count == 0)
            //{
            //    Console.WriteLine("Workflow execution complete for " + startingInput.SourceImageKey);
            //    decisions.Add(CreateCompleteWorkflowExecutionDecision(activityStates));
            //}

            return decisions;
        }

        /// <summary>
        /// Process the history of events to find all completed activity events and the start event. Using that we can find out
        /// what image is being resized and what images still need to be completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="startingInput"></param>
        /// <param name="activityStates"></param>
        void ProcessHistory(DecisionTask task, out WorkflowExecutionStartedInput startingInput, out List<ActivityTaskCompletedResult> activityStates)
        {
            startingInput = null;
            activityStates = new List<ActivityTaskCompletedResult>();

            HistoryIterator iterator = new HistoryIterator(this._swfClient, task);
            foreach (var evnt in iterator)
            {
                if (evnt.EventType == EventType.WorkflowExecutionStarted)
                {
                    startingInput = Utils.DeserializeFromJSON<WorkflowExecutionStartedInput>(evnt.WorkflowExecutionStartedEventAttributes.Input);
                }
                if (evnt.EventType == EventType.ActivityTaskCompleted)
                {
                    ActivityTaskCompletedResult state = Utils.DeserializeFromJSON<ActivityTaskCompletedResult>(evnt.ActivityTaskCompletedEventAttributes.Result);
                    activityStates.Add(state);
                }
            }
        }

        /// <summary>
        /// The method tells SWF what decisions have been made for the current decision task.
        /// </summary>
        /// <param name="taskToken"></param>
        /// <param name="decisions"></param>
        void CompleteTask(string taskToken, List<Decision> decisions)
        {
            RespondDecisionTaskCompletedRequest request = new RespondDecisionTaskCompletedRequest()
            {
                Decisions = decisions,
                TaskToken = taskToken
            };

            this._swfClient.RespondDecisionTaskCompleted(request);
        }

        /// <summary>
        /// Helper method to create a decision for scheduling an activity
        /// </summary>
        /// <returns>Decision with ScheduleActivityTaskDecisionAttributes</returns>
        Decision CreateActivityDecision(WorkflowExecutionStartedInput startingInput, int RequestActionId)
        {
            // setup the input for the activity task.
            ActivityTaskCompletedResult state = new ActivityTaskCompletedResult
            {
                StartingInput = startingInput,
                RequestActionId = RequestActionId
            };

            Decision decision = new Decision()
            {
                DecisionType = DecisionType.ScheduleActivityTask,
                ScheduleActivityTaskDecisionAttributes = new ScheduleActivityTaskDecisionAttributes()
                {
                    ActivityType = new Amazon.SimpleWorkflow.Model.ActivityType()
                    {
                        Name = "MinimalWorkflowActivityType1",
                        Version = "5.0"
                    },
                    ActivityId = "MinimalWorkflowActivityType1" + DateTime.Now.TimeOfDay,
                    Input = Utils.SerializeToJSON<ActivityTaskCompletedResult>(state)
                }
            };
            Console.WriteLine(string.Format("Decision: Schedule Activity Task (RequestId {0} to RequestActionId {1})", state.StartingInput.RequestId, RequestActionId));
            return decision;
        }

        /// <summary>
        /// Helper method to create a decision for completed workflow exeution. This happens once all the thumbnails have been created.
        /// </summary>
        /// <returns>Decision with ScheduleActivityTaskDecisionAttributes</returns>
        Decision CreateCompleteWorkflowExecutionDecision(List<ActivityTaskCompletedResult> states)
        {
            // Create a string listing all the images create.
            StringBuilder sb = new StringBuilder();
            states.ForEach(x => sb.AppendFormat("\tRequestActionId: {0} \r\n", x.RequestActionId));

            Decision decision = new Decision()
            {
                DecisionType = DecisionType.CompleteWorkflowExecution,
                CompleteWorkflowExecutionDecisionAttributes = new CompleteWorkflowExecutionDecisionAttributes
                {
                    Result = sb.ToString()
                }
            };
            Console.WriteLine("Decision: Complete Workflow Execution");
            Console.WriteLine(sb.ToString());
            return decision;
        }

    }
}
