using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.Commands
{
    public class SaveActionCommand : ICommand 
    {
        public Action data { get; set; }
    }
}