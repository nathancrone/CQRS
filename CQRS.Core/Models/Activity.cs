using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Activity
    {
        public int? ActivityId { get; set; }
        public int? ProcessId { get; set; }
        public int? ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Process Process { get; set; }
        public virtual ActivityType ActivityType { get; set; }

        public virtual ICollection<Transition> Transitions { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
