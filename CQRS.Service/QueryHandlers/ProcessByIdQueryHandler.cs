using System.Linq;
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

            //result.Process = _processRepository.Get().Single(x => x.ProcessId == query.Id);

            result.Process = _processRepository.Get().Where(x => x.ProcessId == query.Id).Select(x => new Process() { ProcessId = x.ProcessId, States = x.States }).ToList().FirstOrDefault();

            return result;
        }
    }
}