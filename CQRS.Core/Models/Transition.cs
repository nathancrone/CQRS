using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Transition
    {
        public int TransitionId { get; set; }
        public int? ProcessId { get; set; }
        public int? CurrentStateId { get; set; }
        public int? NextStateId { get; set; }

        public Process Process { get; set; }
        public State CurrentState { get; set; }
        public State NextState { get; set; }

        public ICollection<Action> Actions { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
