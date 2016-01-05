using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Amazon;
using Amazon.SimpleWorkflow;
using Amazon.SimpleWorkflow.Model;

using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Repository;

using CQRS.AWS.DecisionConsole.Shared;

//using Amazon.S3;
//using Amazon.S3.Model;

namespace CQRS.AWS.DecisionConsole
{
    class StartWorkflowExecutionProcessor
    {
        //IAmazonSimpleWorkflow _swfClient = new AmazonSimpleWorkflowClient();


        private WFTracking _tracker;
        //private EFContext _unitOfWork;
        //private readonly GenericRepository<Request> _requestRepository;
        //private readonly GenericRepository<RequestAction> _requestActionRepository;
        //private readonly GenericRepository<State> _stateRepository;
        //private readonly GenericRepository<Transition> _transitionRepository;

        //IAmazonS3 _s3Client = new AmazonS3Client();

        //VirtualConsole _console;

        public StartWorkflowExecutionProcessor()
        {
            _tracker = new WFTracking();
            //_unitOfWork = new EFContext();
            //_requestRepository = new GenericRepository<Request>(_unitOfWork);
            //_requestActionRepository = new GenericRepository<RequestAction>(_unitOfWork);
            //_stateRepository = new GenericRepository<State>(_unitOfWork);
            //_transitionRepository = new GenericRepository<Transition>(_unitOfWork);
        }

        /// <summary>
        /// This method starts the workflow execution.
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="filepath"></param>
        public void StartWorkflowExecution(int RequestId)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //IAmazonS3 s3Client = new AmazonS3Client();
                    IAmazonSimpleWorkflow swfClient = new AmazonSimpleWorkflowClient();

                    _tracker.RequestInitialize(RequestId);

                    //this._console.WriteLine("Create bucket if it doesn't exist");
                    // Make sure bucket exists
                    //s3Client.PutBucket(new PutBucketRequest
                    //{
                    //    BucketName = bucket,
                    //    UseClientRegion = true
                    //});

                    //this._console.WriteLine("Uploading image to S3");
                    // Upload the image to S3 before starting the execution
                    //PutObjectRequest putRequest = new PutObjectRequest
                    //{
                    //    BucketName = bucket,
                    //    FilePath = filepath,
                    //    Key = Path.GetFileName(filepath)
                    //};

                    //// Add upload progress callback to print every increment of 10 percent uploaded to the console.
                    //int currentPercent = -1;
                    //putRequest.StreamTransferProgress = new EventHandler<Amazon.Runtime.StreamTransferProgressArgs>((x, args) =>
                    //{
                    //    if (args.PercentDone == currentPercent)
                    //        return;

                    //    currentPercent = args.PercentDone;
                    //    if (currentPercent % 10 == 0)
                    //    {
                    //        this._console.WriteLine(string.Format("... Uploaded {0} %", currentPercent));
                    //    }
                    //});

                    //s3Client.PutObject(putRequest);

                    // Setup the input for the workflow execution that tells the execution what bukcet and object to use.
                    WorkflowExecutionStartedInput input = new WorkflowExecutionStartedInput
                    {
                        RequestId = RequestId
                    };

                    Console.WriteLine("Start workflow execution");
                    // Start the workflow execution
                    swfClient.StartWorkflowExecution(new StartWorkflowExecutionRequest()
                    {
                        //Serialize input to a string
                        Input = Utils.SerializeToJSON<WorkflowExecutionStartedInput>(input),
                        //Unique identifier for the execution
                        WorkflowId = DateTime.Now.Ticks.ToString(),
                        Domain = Constants.WFDomain,
                        WorkflowType = new WorkflowType()
                        {
                            Name = Constants.WFWorkflow,
                            Version = Constants.WFWorkflowVersion
                        }
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error starting workflow execution: " + e.Message);
                }
            });
        }

        //public void RequestInitialize(int RequestId)
        //{
        //    //get the "request" (include "request actions")...
        //    List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
        //    includes1.Add(x => x.RequestActions);
        //    Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();

        //    if (RequestCurrent.CurrentStateId == null)
        //    {
        //        //find the start state for this request's process (there should only be one start state per process)...
        //        State StateStart = _stateRepository.Get().Where(x => x.ProcessId == RequestCurrent.ProcessId && x.StateTypeId == 1).FirstOrDefault();

        //        RequestSetCurrentState(RequestId, StateStart.StateId ?? 0);
        //    }
        //}

        //public void RequestFollowTransition(int RequestId, int TransitionId)
        //{
        //    //get the "request"...
        //    Request RequestCurrent = _requestRepository.Get().Where(x => x.RequestId == RequestId).FirstOrDefault();

        //    //get the "transition"...
        //    Transition TransitionToFollow = _transitionRepository.Get().Where(x => x.TransitionId == TransitionId).FirstOrDefault();
            
        //    //if the request's current state matches the transition and next state is not null
        //    if (RequestCurrent.CurrentStateId == TransitionToFollow.CurrentStateId && TransitionToFollow.NextStateId != null)
        //    {
        //        RequestSetCurrentState(RequestId, TransitionToFollow.NextStateId ?? 0);
        //    }
        //}

        //public void RequestActionComplete(int RequestActionId)
        //{
        //    //get the request action
        //    RequestAction RequestActionCompleted = _requestActionRepository.Get().Where(x => x.RequestActionId == RequestActionId).FirstOrDefault();

        //    //get the requestid
        //    int RequestId = RequestActionCompleted.RequestId ?? 0;

        //    //get the actionid
        //    int ActionId = RequestActionCompleted.ActionId ?? 0;

        //    //get the transitionid
        //    int TransitionId = RequestActionCompleted.TransitionId ?? 0;

        //    //set that entry's IsActive = 0 and IsCompleted = 1.
        //    RequestActionCompleted.IsActive = false;
        //    RequestActionCompleted.IsComplete = true;

        //    //save the change
        //    _unitOfWork.SaveChanges();

        //    //After marking the submitted Action as completed, we check all Actions for that Transition in that Request. 
        //    //If all RequestActions are marked as Completed, then we disable all remaining actions 
        //    //(by setting IsActive = 0, e.g. all actions for Transitions that were not matched).

        //    //get the "request" (include "request actions")...
        //    List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
        //    includes1.Add(x => x.RequestActions);
        //    Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();

        //    //If all RequestActions are marked as Completed
        //    if (RequestCurrent.RequestActions.Where(x => x.ActionId == ActionId && x.TransitionId == TransitionId).All(x => x.IsComplete))
        //    {
        //        //setting IsActive = 0, e.g. all actions for Transitions that were not matched
        //        foreach (RequestAction ra in RequestCurrent.RequestActions.Where(x => x.IsActive))
        //        {
        //            ra.IsActive = false;
        //        }

        //        _unitOfWork.SaveChanges();

        //        //follow the transition
        //        Transition TransitionToFollow = _transitionRepository.Get().Where(x => x.TransitionId == TransitionId).FirstOrDefault();
        //        if (TransitionToFollow.NextStateId != null)
        //        {
        //            RequestSetCurrentState(RequestId, TransitionToFollow.NextStateId ?? 0);
        //        }
        //    }
        //}

        //public void RequestSetCurrentState(int RequestId, int StateId)
        //{
        //    //get the "request" (include "request actions")...
        //    List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
        //    includes1.Add(x => x.RequestActions);
        //    Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();

        //    //find the new state...
        //    List<Expression<Func<State, object>>> includes2 = new List<Expression<Func<State, object>>>();
        //    includes2.Add(x => x.TransitionsFrom);
        //    includes2.Add(x => x.TransitionsFrom.Select(y => y.Actions));
        //    State StateNext = _stateRepository.Get(filter: x => x.StateId == StateId, orderBy: null, includes: includes2.ToArray()).FirstOrDefault();

        //    //set the current state of the request to the new state
        //    RequestCurrent.CurrentStateId = StateId;

        //    //For each Action in those Transitions, we add an entry in RequestAction, with each entry having IsActive = 1 and IsCompleted = 0
        //    foreach (Transition t in StateNext.TransitionsFrom)
        //    {
        //        foreach (CQRS.Core.Models.Action a in t.Actions)
        //        {
        //            RequestCurrent.RequestActions.Add(new RequestAction()
        //            {
        //                RequestId = RequestCurrent.RequestId,
        //                ActionId = a.ActionId,
        //                TransitionId = t.TransitionId,
        //                IsActive = true,
        //                IsComplete = false
        //            });
        //        }
        //    }

        //    _unitOfWork.SaveChanges();
        //}

    }
}
