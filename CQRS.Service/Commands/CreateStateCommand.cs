using System;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.Commands
{
    public class CreateStateCommand : ICommand 
    {
        public State State { get; set; }
    }
}