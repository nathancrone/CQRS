using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class State
    {
        public int? StateId { get; set; }
        public int? ProcessId { get; set; }
        public int? StateTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Process Process { get; set; }
        public virtual StateType StateType { get; set; }

        public virtual ICollection<Transition> Transitions { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
