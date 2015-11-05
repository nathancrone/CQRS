using CQRS.Core;
using CQRS.Service.Queries;
using CQRS.Service.QueryResults;
using System.Web.Mvc;

namespace CQRS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public HomeController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        // GET: Home
        public ActionResult Index(TasksByStatusQuery query)
        {
            var result = _queryDispatcher.Dispatch<TasksByStatusQuery, TasksByStatusQueryResult>(query);
            return View(result);
        }
    }
}