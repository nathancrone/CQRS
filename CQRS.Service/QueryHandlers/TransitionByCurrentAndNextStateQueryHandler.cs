using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Queries;
using CQRS.Service.QueryResults;

namespace CQRS.Service.QueryHandlers
{
    public class TransitionByCurrentAndNextStateQueryHandler : IQueryHandler<TransitionByCurrentAndNextStateQuery, TransitionByCurrentAndNextStateQueryResult>
    {
        private readonly IGenericRepository<Transition> _transitionRepository;

        public TransitionByCurrentAndNextStateQueryHandler(IGenericRepository<Transition> transitionRepository)
        {
            _transitionRepository = transitionRepository;
        }

        public TransitionByCurrentAndNextStateQueryResult Retrieve(TransitionByCurrentAndNextStateQuery query)
        {
            //creating a list of "includes" to include actions
            List<Expression<Func<Transition, object>>> includes = new List<Expression<Func<Transition, object>>>();
            includes.Add(x => x.Actions);

            TransitionByCurrentAndNextStateQueryResult result = new TransitionByCurrentAndNextStateQueryResult();
            result.Transition = _transitionRepository
                .Get(
                filter: x => x.CurrentStateId == query.CurrentStateId && x.NextStateId == query.NextStateId,
                orderBy: null,
                includes: includes.ToArray())
                .FirstOrDefault();

            return result;
        }
    }
}