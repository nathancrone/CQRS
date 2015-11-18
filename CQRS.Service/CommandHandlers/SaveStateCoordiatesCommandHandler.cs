using System;
using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;

namespace CQRS.Service.CommandHandlers
{
    public class SaveStateCoordinatesCommandHandler : ICommandHandler<SaveStateCoordinatesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<State> _stateRepository;
        
        public SaveStateCoordinatesCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<State> stateRepository)
        {
            if (stateRepository == null) { throw new ArgumentNullException("stateRepository"); }
            _stateRepository = stateRepository;
            _unitOfWork = unitOfWork;
        }

        public void Execute(SaveStateCoordinatesCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }
            if (command.Coordinates == null) { throw new ArgumentException("Coordinates is not specified", "command"); }
            
            foreach (var s in command.Coordinates)
            {
                _stateRepository.Update(s, new string[] { "X", "Y" });
            }

            _unitOfWork.SaveChanges();
        }

    }
}

