using System;
using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;

namespace CQRS.Service.CommandHandlers
{
    public class SaveProcessCommandHandler : ICommandHandler<SaveProcessCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Process> _processRepository;

        public SaveProcessCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Process> processRepository)
        {
            if (processRepository == null) { throw new ArgumentNullException("processRepository"); }
            _processRepository = processRepository;
            _unitOfWork = unitOfWork;
        }

        public void Execute(SaveProcessCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }
            if (command.Process == null) { throw new ArgumentException("Process is not specified", "command"); }
            
            if (command.Process.ProcessId <= 0)
            {
                _processRepository.Insert(command.Process);
            }
            else
            {
                _processRepository.Update(command.Process);
            }

            _unitOfWork.SaveChanges();
        }

    }
}

