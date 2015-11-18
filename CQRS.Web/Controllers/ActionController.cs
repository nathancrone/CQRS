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
    public class ActionController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ActionController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public ActionResult JsonSave(SaveActionCommand command)
        {
            _commandDispatcher.Dispatch(command);
            return Content(JsonConvert.SerializeObject(command.data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects }), "application/json");
        }
    }
}