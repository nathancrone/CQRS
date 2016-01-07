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

namespace CQRS.AWS.DecisionConsole.Shared
{
    public class WFTracking
    {
        private EFContext _unitOfWork;
        private readonly GenericRepository<Request> _requestRepository;
        private readonly GenericRepository<RequestAction> _requestActionRepository;
        private readonly GenericRepository<State> _stateRepository;
        private readonly GenericRepository<Transition> _transitionRepository;

        public WFTracking()
        {
            _unitOfWork = new EFContext();
            _requestRepository = new GenericRepository<Request>(_unitOfWork);
            _requestActionRepository = new GenericRepository<RequestAction>(_unitOfWork);
            _stateRepository = new GenericRepository<State>(_unitOfWork);
            _transitionRepository = new GenericRepository<Transition>(_unitOfWork);
        }

        public void RequestInitialize(int RequestId)
        {
            //get the "request" (include "request actions")...
            List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
            includes1.Add(x => x.RequestActions);
            Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();

            //find the start state for this request's process (there should only be one start state per process)...
            State StateStart = _stateRepository.Get().Where(x => x.ProcessId == RequestCurrent.ProcessId && x.StateTypeId == 1).FirstOrDefault();

            RequestSetCurrentState(RequestId, StateStart.StateId ?? 0);
        }

        public void RequestFollowTransition(int RequestId, int TransitionId)
        {

            //get the "request" (include "request actions")...
            List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
            includes1.Add(x => x.RequestActions);
            Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();

            //get the "transition"...
            Transition TransitionToFollow = _transitionRepository.Get().Where(x => x.TransitionId == TransitionId).FirstOrDefault();

            //if the request's current state matches the transition's current state and the transtion's next state is not null
            if (RequestCurrent.CurrentStateId == TransitionToFollow.CurrentStateId && TransitionToFollow.NextStateId != null)
            {
                //setting IsActive = 0, e.g. all actions
                foreach (RequestAction ra in RequestCurrent.RequestActions.Where(x => x.IsActive))
                {
                    ra.IsActive = false;
                }

                _unitOfWork.SaveChanges();

                RequestSetCurrentState(RequestId, TransitionToFollow.NextStateId ?? 0);
            }
        }

        public void RequestActionComplete(int RequestActionId)
        {
            //get the request action
            RequestAction RequestActionCompleted = _requestActionRepository.Get().Where(x => x.RequestActionId == RequestActionId).FirstOrDefault();

            ////get the requestid
            //int RequestId = RequestActionCompleted.RequestId ?? 0;

            ////get the actionid
            //int ActionId = RequestActionCompleted.ActionId ?? 0;

            ////get the transitionid
            //int TransitionId = RequestActionCompleted.TransitionId ?? 0;

            //set that entry's IsActive = 0 and IsCompleted = 1.
            //RequestActionCompleted.IsActive = false;
            RequestActionCompleted.IsComplete = true;

            //save the change
            _unitOfWork.SaveChanges();

            ////After marking the submitted Action as completed, we check all Actions for that Transition in that Request. 
            ////If all RequestActions are marked as Completed, then we disable all remaining actions 
            ////(by setting IsActive = 0, e.g. all actions for Transitions that were not matched).

            ////get the "request" (include "request actions")...
            //List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
            //includes1.Add(x => x.RequestActions);
            //Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();

            ////If all RequestActions are marked as Completed
            //if (RequestCurrent.RequestActions.Where(x => x.ActionId == ActionId && x.TransitionId == TransitionId).All(x => x.IsComplete))
            //{
            //    //setting IsActive = 0, e.g. all actions for Transitions that were not matched
            //    foreach (RequestAction ra in RequestCurrent.RequestActions.Where(x => x.IsActive))
            //    {
            //        ra.IsActive = false;
            //    }

            //    _unitOfWork.SaveChanges();

            //    //follow the transition
            //    Transition TransitionToFollow = _transitionRepository.Get().Where(x => x.TransitionId == TransitionId).FirstOrDefault();
            //    if (TransitionToFollow.NextStateId != null)
            //    {
            //        RequestSetCurrentState(RequestId, TransitionToFollow.NextStateId ?? 0);
            //    }
            //}
        }

        //should return null if there are no completed transitions for the request
        //otherwise it should return the id of the first transition found with all completed actions
        //hopefully there should only ever be one transition to take.
        public Transition GetCompletedTransitionForRequest(int RequestId)
        {
            List<RequestAction> RequestActionsActive = _requestActionRepository.Get().Where(x => x.RequestId == RequestId && x.IsActive).ToList();

            Transition Transition = null;

            IEnumerable<IGrouping<int?, RequestAction>> result = RequestActionsActive.GroupBy(x => x.TransitionId).Where(x => x.All(y => y.IsComplete));

            if (result.Count() != 0)
            {
                Transition = _transitionRepository.Get().Where(x => x.TransitionId == result.FirstOrDefault().Key.Value).FirstOrDefault();
            }

            return Transition;
        }

        public void RequestSetCurrentState(int RequestId, int StateId)
        {
            //get the "request" (include "request actions")...
            List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
            includes1.Add(x => x.RequestActions);
            Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();

            //find the new state (inclode associated transitions and their actions)...
            List<Expression<Func<State, object>>> includes2 = new List<Expression<Func<State, object>>>();
            includes2.Add(x => x.TransitionsFrom);
            includes2.Add(x => x.TransitionsFrom.Select(y => y.Actions));
            State StateNext = _stateRepository.Get(filter: x => x.StateId == StateId, orderBy: null, includes: includes2.ToArray()).FirstOrDefault();

            //set the current state of the request to the new state
            RequestCurrent.CurrentStateId = StateId;

            //For each Action in those Transitions, we add an entry in RequestAction, with each entry having IsActive = 1 and IsCompleted = 0
            foreach (Transition t in StateNext.TransitionsFrom)
            {
                foreach (CQRS.Core.Models.Action a in t.Actions)
                {
                    RequestCurrent.RequestActions.Add(new RequestAction()
                    {
                        RequestId = RequestCurrent.RequestId,
                        ActionId = a.ActionId,
                        TransitionId = t.TransitionId,
                        IsActive = true,
                        IsComplete = false
                    });
                }
            }

            _unitOfWork.SaveChanges();
        }

        public State GetState(int StateId)
        {
            //find the new state (include associated transitions)...
            List<Expression<Func<State, object>>> includes2 = new List<Expression<Func<State, object>>>();
            includes2.Add(x => x.TransitionsFrom);
            includes2.Add(x => x.TransitionsTo);
            State State = _stateRepository.Get(filter: x => x.StateId == StateId, orderBy: null, includes: includes2.ToArray()).FirstOrDefault();
            return State;
        }

        public Request GetRequestWithActions(int RequestId)
        {
            //get the request with request actions...
            List<Expression<Func<Request, object>>> includes1 = new List<Expression<Func<Request, object>>>();
            includes1.Add(x => x.RequestActions);
            Request RequestCurrent = _requestRepository.Get(filter: x => x.RequestId == RequestId, orderBy: null, includes: includes1.ToArray()).FirstOrDefault();
            return RequestCurrent;
        }

    }
}
