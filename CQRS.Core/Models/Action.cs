using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Action
    {
        public int? ActionId { get; set; }
        public int? ProcessId { get; set; }
        public int? ActionTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Process Process { get; set; }
        public virtual ActionType ActionType { get; set; }

        public virtual ICollection<Transition> Transitions { get; set; }
    }
}
