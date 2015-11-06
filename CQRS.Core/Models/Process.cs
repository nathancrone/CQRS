using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Process 
    {
        public int? ProcessId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> ProcessAdmins { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Transition> Transitions { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
