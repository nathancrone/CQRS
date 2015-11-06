using CQRS.Core;

namespace CQRS.Service.Queries
{
    public class ByIdQuery : IQuery
    {
        public int Id { get; set; }
    }
}