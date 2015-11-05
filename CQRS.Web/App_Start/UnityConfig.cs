using CQRS.Core;
using CQRS.Web.Infrastructure;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc5;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CQRS.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //container.RegisterType<ICommandDispatcher, CommandDispatcher>();
            //container.RegisterType<IQueryDispatcher, QueryDispatcher>();

            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies().Where(x => !x.GetInterfaces().Contains(typeof(IModuleInit))),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            ModuleLoader.LoadContainer(container, ".\\bin", "C*.dll");

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}