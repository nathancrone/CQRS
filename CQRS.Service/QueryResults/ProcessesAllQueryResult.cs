using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.QueryResults
{
    public class ProcessesAllQueryResult : IQueryResult
    {
        public IEnumerable<Process> Processes { get; set; }
    }
}