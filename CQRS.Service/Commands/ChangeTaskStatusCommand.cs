using System;
using CQRS.Core;

namespace CQRS.Service.Commands
{
    public class ChangeTaskStatusCommand : ICommand 
    {
        public int TaskId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}