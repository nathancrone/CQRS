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

        public Process Process { get; set; }
        public ActionType ActionType { get; set; }

        public ICollection<Transition> Transitions { get; set; }
    }
}
