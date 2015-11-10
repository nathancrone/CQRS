using System;
using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Group
    {
        public int? GroupId { get; set; }
        public int? ProcessId { get; set; }
        public string Name { get; set; }

        public Process Process { get; set; }

        public ICollection<User> Members { get; set; }
        public ICollection<ActionTarget> ActionTargets { get; set; }
        public ICollection<ActivityTarget> ActivityTargets { get; set; }
    }
}
