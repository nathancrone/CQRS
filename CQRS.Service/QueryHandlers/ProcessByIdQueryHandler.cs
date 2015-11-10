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
    public class ProcessByIdQueryHandler : IQueryHandler<ByIdQuery, ProcessByIdQueryResult>
    {
        private readonly IGenericRepository<Process> _processRepository;

        public ProcessByIdQueryHandler(IGenericRepository<Process> processRepository)
        {
            _processRepository = processRepository;
        }

        public ProcessByIdQueryResult Retrieve(ByIdQuery query)
        {
            ProcessByIdQueryResult result = new ProcessByIdQueryResult();

            //creating a list of "includes" to include states actions and transitions
            List<Expression<Func<Process, object>>> includes = new List<Expression<Func<Process, object>>>();
            includes.Add(x => x.States);
            includes.Add(x => x.States.Select(a => a.TransitionsFrom));
            includes.Add(x => x.States.Select(a => a.TransitionsTo));
            includes.Add(x => x.Transitions);
            includes.Add(x => x.Transitions.Select(a => a.Actions));

            //filter by process id and include specified entities
            result.Process = _processRepository.Get(filter: x => x.ProcessId == query.Id, orderBy: null, includes: includes.ToArray()).FirstOrDefault();

            return result;
        }
    }
}