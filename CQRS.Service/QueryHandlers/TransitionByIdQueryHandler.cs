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
    public class TransitionByIdQueryHandler : IQueryHandler<ByIdQuery, TransitionByIdQueryResult>
    {
        private readonly IGenericRepository<Transition> _transitionRepository;

        public TransitionByIdQueryHandler(IGenericRepository<Transition> transitionRepository)
        {
            _transitionRepository = transitionRepository;
        }

        public TransitionByIdQueryResult Retrieve(ByIdQuery query)
        {
            TransitionByIdQueryResult result = new TransitionByIdQueryResult();

            //creating a list of "includes" to include states actions and transitions
            List<Expression<Func<Transition, object>>> includes = new List<Expression<Func<Transition, object>>>();
            includes.Add(x => x.Actions);
            //includes.Add(x => x.States.Select(a => a.TransitionsFrom));
            //includes.Add(x => x.States.Select(a => a.TransitionsTo));
            //includes.Add(x => x.Transitions);
            //includes.Add(x => x.Transitions.Select(a => a.Actions));

            //filter by process id and include specified entities
            result.Transition = _transitionRepository.Get(filter: x => x.TransitionId == query.Id, orderBy: null, includes: includes.ToArray()).FirstOrDefault();

            return result;
        }
    }
}