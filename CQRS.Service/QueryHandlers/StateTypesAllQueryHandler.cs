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
    public class StateTypesAllQueryHandler : IQueryHandler<EmptyQuery, StateTypesAllQueryResult>
    {
        private readonly IGenericRepository<StateType> _stateTypeRepository;

        public StateTypesAllQueryHandler(IGenericRepository<StateType> stateTypeRepository)
        {
            _stateTypeRepository = stateTypeRepository;
        }

        public StateTypesAllQueryResult Retrieve(EmptyQuery query)
        {
            StateTypesAllQueryResult result = new StateTypesAllQueryResult();
            result.StateTypes = _stateTypeRepository.Get().ToList();
            return result;
        }
    }
}