using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Queries;
using CQRS.Service.QueryResults;
using CQRS.Service.QueryHandlers;
using System.ComponentModel.Composition;

namespace CQRS.Service
{
    [Export(typeof(IModuleInit))]
    public class ModuleInit : IModuleInit
    {
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterType<IQueryHandler<TasksByStatusQuery, TasksByStatusQueryResult>, TasksByStatusQueryHandler>();
            registrar.RegisterType<IQueryHandler<ByIdQuery, ProcessByIdQueryResult>, ProcessByIdQueryHandler>();
        }
    }
}
