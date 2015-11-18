using System;
using System.Linq;
using CQRS.Core;
using Models = CQRS.Core.Models;
using CQRS.Service.Commands;

namespace CQRS.Service.CommandHandlers
{
    public class SaveActionCommandHandler : ICommandHandler<SaveActionCommand>
    {
        private readonly IGenericRepository<Models.Action> _actionRepository;

        public SaveActionCommandHandler(IGenericRepository<Models.Action> actionRepository)
        {
            if (actionRepository == null) { throw new ArgumentNullException("actionRepository"); }
            _actionRepository = actionRepository;
        }

        public void Execute(SaveActionCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }
            if (command.data == null) { throw new ArgumentException("Action is not specified", "command"); }
            
            if (command.data.ActionId <= 0)
            {
                _actionRepository.Insert(command.data);
            }
            else
            {
                _actionRepository.Update(command.data);
            }
            
            _actionRepository.Save();
        }

    }
}

