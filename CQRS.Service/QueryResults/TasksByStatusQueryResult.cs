using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.QueryResults
{
    public class TasksByStatusQueryResult : IQueryResult
    {
        public DateTime LastUpdateForAnyTask { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}