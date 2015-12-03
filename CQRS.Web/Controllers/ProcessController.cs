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
    public class ProcessController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ProcessController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        // GET: Home
        public ActionResult Flowchart()
        {
            return View();
        }

        public ActionResult JsonFlowchart(int id)
        {
            var result = _queryDispatcher.Dispatch<ByIdQuery, ProcessByIdQueryResult>(new ByIdQuery() { Id = id });

            //track how many input connectors for each state
            Dictionary<int, int> IndexCurrent = result.Process.Transitions
                .GroupBy(x => x.CurrentStateId)
                .Select(x => x.Key.Value)
                .ToDictionary(x => x, x => 0);

            //track how many output connectors for each state
            Dictionary<int, int> IndexNext = result.Process.Transitions
                .GroupBy(x => x.NextStateId)
                .Select(x => x.Key.Value)
                .ToDictionary(x => x, x => 0);

            var data = new
            {
                nodes = result.Process.States.Select(x => new
                {
                    name = x.Name,
                    description = x.Description, 
                    id = x.StateId,
                    statetypeid = x.StateTypeId, 
                    x = x.X,
                    y = x.Y,
                    inputConnectors = x.TransitionsTo.Select(a => new { name = a.TransitionId }),
                    outputConnectors = x.TransitionsFrom.Select(a => new { name = a.TransitionId })
                }).ToArray(),
                connections = result.Process.Transitions.Select(x => new
                {
                    id = x.TransitionId, 
                    source = new { nodeID = x.CurrentStateId, connectorIndex = IndexCurrent[x.CurrentStateId ?? 0]++ },
                    dest = new { nodeID = x.NextStateId, connectorIndex = IndexNext[x.NextStateId ?? 0]++ }
                }).ToArray()
            };

            return Content(JsonConvert.SerializeObject(
                new {
                    Process = new {
                        ProcessId = result.Process.ProcessId, 
                        Name = result.Process.Name
                    },
                    ViewModel = data }
                ), "application/json");
        }

        [HttpGet]
        public ActionResult JsonAll(EmptyQuery query)
        {
            var result = _queryDispatcher.Dispatch<EmptyQuery, ProcessesAllQueryResult>(query);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [HttpPost]
        public ActionResult JsonSave(SaveProcessCommand command)
        {
            _commandDispatcher.Dispatch(command);
            return Content(JsonConvert.SerializeObject(command.Process), "application/json");
        }
    }
}