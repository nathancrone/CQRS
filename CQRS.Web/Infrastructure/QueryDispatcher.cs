using System;
using Microsoft.Practices.Unity;
using CQRS.Core;

namespace CQRS.Web.Infrastructure
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IUnityContainer _kernel;

        public QueryDispatcher(IUnityContainer kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = _kernel.Resolve<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }
    }
}