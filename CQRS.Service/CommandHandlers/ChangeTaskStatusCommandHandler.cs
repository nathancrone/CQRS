using System;
using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;

namespace CQRS.Service.CommandHandlers
{
    public class ChangeTaskStatusCommandHandler : ICommandHandler<ChangeTaskStatusCommand>
    {
        private readonly IGenericRepository<Task> _taskRepository;

        public ChangeTaskStatusCommandHandler(IGenericRepository<Task> taskRepository)
        {
            if (taskRepository == null) { throw new ArgumentNullException("taskRepository"); }
            _taskRepository = taskRepository;
        }

        public void Execute(ChangeTaskStatusCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }
            if (command.TaskId == 0) { throw new ArgumentException("Id is not specified", "command"); }

            var task = _taskRepository.Get().Single(x => x.TaskId == command.TaskId);
            task.IsCompleted = command.IsCompleted;
            task.LastUpdated = command.UpdatedOn;

            _taskRepository.Update(task);
        }

    }
}

