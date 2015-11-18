using System;
using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;

namespace CQRS.Service.CommandHandlers
{
    public class SaveStateCommandHandler : ICommandHandler<SaveStateCommand>
    {
        private readonly IGenericRepository<State> _stateRepository;

        public SaveStateCommandHandler(IGenericRepository<State> stateRepository)
        {
            if (stateRepository == null) { throw new ArgumentNullException("stateRepository"); }
            _stateRepository = stateRepository;
        }

        public void Execute(SaveStateCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }
            if (command.State == null) { throw new ArgumentException("State is not specified", "command"); }
            
            if (command.State.StateId <= 0)
            {
                _stateRepository.Insert(command.State);
            }
            else
            {
                _stateRepository.Update(command.State);
            }
            
            _stateRepository.Save();
        }

    }
}

