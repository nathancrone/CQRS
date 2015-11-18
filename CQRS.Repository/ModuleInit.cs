using System.ComponentModel.Composition;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Repository
{
    [Export(typeof(IModuleInit))]
    public class ModuleInit : IModuleInit
    {
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterTypeWithPerRequestLife<IUnitOfWork, EFContext>();
            //registrar.RegisterType<IContext, EFContext>();
            registrar.RegisterType<IGenericRepository<Task>, GenericRepository<Task>>();
            registrar.RegisterType<IGenericRepository<Process>, GenericRepository<Process>>();
            registrar.RegisterType<IGenericRepository<Transition>, GenericRepository<Transition>>();
            registrar.RegisterType<IGenericRepository<State>, GenericRepository<State>>();
        }
    }
}
