using CQRS.Core;
using Microsoft.Practices.Unity;
using System;

namespace CQRS.Web.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IUnityContainer _kernel;

        public CommandDispatcher(IUnityContainer kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = _kernel.Resolve<ICommandHandler<TParameter>>();
            handler.Execute(command);
        }
    }
}