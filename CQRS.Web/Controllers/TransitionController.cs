using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Service.Queries;
using CQRS.Service.QueryResults;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Linq.Expressions;

namespace CQRS.Web.Controllers
{
    public class TransitionController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public TransitionController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;

        }

        [HttpPost]
        public ActionResult JsonByCurrentAndNextState(TransitionByCurrentAndNextStateQuery query)
        {
            var result = _queryDispatcher.Dispatch<TransitionByCurrentAndNextStateQuery, TransitionByCurrentAndNextStateQueryResult>(query);
            return Content(JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects }), "application/json");
        }
    }
}