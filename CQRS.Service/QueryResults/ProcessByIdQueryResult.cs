using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.QueryResults
{
    public class ProcessByIdQueryResult : IQueryResult
    {
        public Process Process { get; set; }
    }
}