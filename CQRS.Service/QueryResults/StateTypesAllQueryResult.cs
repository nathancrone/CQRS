using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.QueryResults
{
    public class StateTypesAllQueryResult : IQueryResult
    {
        public IEnumerable<StateType> StateTypes { get; set; }
    }
}