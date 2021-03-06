﻿using System;
using System.Linq;
using CQRS.Core;
using CQRS.Core.Models;
using CQRS.Service.Commands;

namespace CQRS.Service.CommandHandlers
{
    public class SaveTransitionCommandHandler : ICommandHandler<SaveTransitionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Transition> _transitionRepository;

        public SaveTransitionCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Transition> transitionRepository)
        {
            if (transitionRepository == null) { throw new ArgumentNullException("transitionRepository"); }
            _transitionRepository = transitionRepository;
            _unitOfWork = unitOfWork;
        }

        public void Execute(SaveTransitionCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }
            if (command.Transition == null) { throw new ArgumentException("Transition is not specified", "command"); }
            
            if (command.Transition.TransitionId <= 0)
            {
                _transitionRepository.Insert(command.Transition);
            }
            else
            {
                _transitionRepository.Update(command.Transition);
            }

            _unitOfWork.SaveChanges();
        }

    }
}

