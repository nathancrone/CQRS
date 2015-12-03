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
    public class ActionTypesAllQueryHandler : IQueryHandler<EmptyQuery, ActionTypesAllQueryResult>
    {
        private readonly IGenericRepository<ActionType> _actionTypeRepository;

        public ActionTypesAllQueryHandler(IGenericRepository<ActionType> ActionTypeRepository)
        {
            _actionTypeRepository = ActionTypeRepository;
        }

        public ActionTypesAllQueryResult Retrieve(EmptyQuery query)
        {
            ActionTypesAllQueryResult result = new ActionTypesAllQueryResult();
            result.ActionTypes = _actionTypeRepository.Get().ToList();
            return result;
        }
    }
}