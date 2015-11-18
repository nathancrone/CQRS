using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;
using CQRS.Service.CommandHandlers;
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
            registrar.RegisterType<IQueryHandler<ByIdQuery, TransitionByIdQueryResult>, TransitionByIdQueryHandler>();

            registrar.RegisterType<ICommandHandler<SaveStateCommand>, SaveStateCommandHandler>();
            registrar.RegisterType<ICommandHandler<SaveStateCoordinatesCommand>, SaveStateCoordinatesCommandHandler>();
            registrar.RegisterType<ICommandHandler<SaveTransitionCommand>, SaveTransitionCommandHandler>();
            registrar.RegisterType<ICommandHandler<SaveActionCommand>, SaveActionCommandHandler>();
        }
    }
}
