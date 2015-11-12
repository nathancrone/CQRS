using CQRS.Core;

namespace CQRS.Service.Queries
{
    public class TransitionByCurrentAndNextStateQuery : IQuery
    {
        public int CurrentStateId { get; set; }
        public int NextStateId { get; set; }
    }
}