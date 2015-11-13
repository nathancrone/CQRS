using System;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.Commands
{
    public class SaveTransitionCommand : ICommand 
    {
        public Transition Transition { get; set; }
    }
}