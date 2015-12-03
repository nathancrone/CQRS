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
    public class StateTypeController : Controller
    {

        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;


        public StateTypeController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public ActionResult JsonAll(EmptyQuery query)
        {
            var result = _queryDispatcher.Dispatch<EmptyQuery, StateTypesAllQueryResult>(query);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}