using CQRS.Core;

namespace CQRS.Service.Queries
{
    public class TasksByStatusQuery : IQuery
    {
        public bool IsCompleted { get; set; }
    }
}