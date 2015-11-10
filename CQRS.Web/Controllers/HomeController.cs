using CQRS.Core;
using CQRS.Service.Queries;
using CQRS.Service.QueryResults;
using System.Web.Mvc;

namespace CQRS.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}