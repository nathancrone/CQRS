using System;
using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;

namespace CQRS.Service.CommandHandlers
{
    public class CreateStateCommandHandler : ICommandHandler<CreateStateCommand>
    {
        private readonly IGenericRepository<State> _stateRepository;

        public CreateStateCommandHandler(IGenericRepository<State> stateRepository)
        {
            if (stateRepository == null) { throw new ArgumentNullException("stateRepository"); }
            _stateRepository = stateRepository;
        }

        public void Execute(CreateStateCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }
            if (command.State == null) { throw new ArgumentException("State is not specified", "command"); }
            
            _stateRepository.Insert(command.State);
            _stateRepository.Save();
        }

    }
}

