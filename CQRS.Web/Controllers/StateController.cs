using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Linq.Expressions;

namespace CQRS.Web.Controllers
{
    public class StateController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public StateController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public ActionResult JsonCreate(State s)
        {
            var command = new CreateStateCommand() { State = s };
            _commandDispatcher.Dispatch<CreateStateCommand>(command);
            return Content(JsonConvert.SerializeObject(command.State.StateId), "application/json");
        }
    }
}