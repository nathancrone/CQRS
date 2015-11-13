using System;
using CQRS.Core;
using CQRS.Core.Models;
using System.Collections.Generic;

namespace CQRS.Service.Commands
{
    public class SaveStateCoordinatesCommand : ICommand 
    {
        public State[] Coordinates { get; set; }
    }
}
