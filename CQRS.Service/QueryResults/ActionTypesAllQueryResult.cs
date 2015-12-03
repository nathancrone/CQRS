using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.QueryResults
{
    public class ActionTypesAllQueryResult : IQueryResult
    {
        public IEnumerable<ActionType> ActionTypes { get; set; }
    }
}