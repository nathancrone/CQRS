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
    public class ProcessesAllQueryHandler : IQueryHandler<EmptyQuery, ProcessesAllQueryResult>
    {
        private readonly IGenericRepository<Process> _processRepository;

        public ProcessesAllQueryHandler(IGenericRepository<Process> processRepository)
        {
            _processRepository = processRepository;
        }

        public ProcessesAllQueryResult Retrieve(EmptyQuery query)
        {
            ProcessesAllQueryResult result = new ProcessesAllQueryResult();
            result.Processes = _processRepository.Get().ToList();
            return result;
        }
    }
}