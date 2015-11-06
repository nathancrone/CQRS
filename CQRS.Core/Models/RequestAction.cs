using System;
using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class RequestAction
    {
        public int? RequestActionId { get; set; }
        public int? RequestId { get; set; }
        public int? ActionId { get; set; }
        public int? TransitionId { get; set; }

        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }

        public virtual Request Request { get; set; }
        public virtual Action Action { get; set; }
        public virtual Transition Transition { get; set; }
    }
}
