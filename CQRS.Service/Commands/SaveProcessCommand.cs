using System;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.Commands
{
    public class SaveProcessCommand : ICommand 
    {
        public Process Process { get; set; }
    }
}