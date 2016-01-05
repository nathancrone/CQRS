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
        public int X { get; set; }
        public int Y { get; set; }

        public Process Process { get; set; }
        public StateType StateType { get; set; }
        
        public ICollection<Transition> TransitionsFrom { get; set; }
        public ICollection<Transition> TransitionsTo { get; set; }

        //public ICollection<Action> Actions { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
