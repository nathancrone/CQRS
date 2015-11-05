using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Queries;
using CQRS.Service.QueryResults;

namespace CQRS.Service.QueryHandlers
{
    public class TasksByStatusQueryHandler : IQueryHandler<TasksByStatusQuery, TasksByStatusQueryResult>
    {
        private readonly IGenericRepository<Task> _taskRepository;

        public TasksByStatusQueryHandler(IGenericRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TasksByStatusQueryResult Retrieve(TasksByStatusQuery query)
        {
            TasksByStatusQueryResult result = new TasksByStatusQueryResult();
            result.Tasks = _taskRepository.Get().Where(x => x.IsCompleted == query.IsCompleted).ToList();
            result.LastUpdateForAnyTask = _taskRepository.Get().Max(x => x.LastUpdated);
            return result;
        }
    }
}