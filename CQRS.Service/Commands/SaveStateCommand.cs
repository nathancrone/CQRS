using System;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.Commands
{
    public class SaveStateCommand : ICommand 
    {
        public State State { get; set; }
    }
}