using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Service.Commands;
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
        public ActionResult JsonById(ByIdQuery query)
        {
            var result = _queryDispatcher.Dispatch<ByIdQuery, TransitionByIdQueryResult>(query);
            return Content(JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects }), "application/json");
        }


        [HttpPost]
        public ActionResult JsonSave(SaveTransitionCommand command)
        {
            _commandDispatcher.Dispatch(command);
            return Content(JsonConvert.SerializeObject(command.Transition), "application/json");
        }
    }
}