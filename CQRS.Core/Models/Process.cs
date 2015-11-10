using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Process 
    {
        public int? ProcessId { get; set; }
        public string Name { get; set; }

        public ICollection<User> ProcessAdmins { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<State> States { get; set; }
        public ICollection<Transition> Transitions { get; set; }
        public ICollection<Action> Actions { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
