using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Transition
    {
        public int? TransitionId { get; set; }
        public int? ProcessId { get; set; }
        public int? CurrentStateId { get; set; }
        public int? NextStateId { get; set; }

        public virtual Process Process { get; set; }
        public virtual State CurrentState { get; set; }
        public virtual State NextState { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
