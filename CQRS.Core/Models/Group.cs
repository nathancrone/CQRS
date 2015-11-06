using System;
using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Group
    {
        public int? GroupId { get; set; }
        public int? ProcessId { get; set; }
        public string Name { get; set; }

        public virtual Process Process { get; set; }

        public virtual ICollection<User> Members { get; set; }
        public virtual ICollection<ActionTarget> ActionTargets { get; set; }
        public virtual ICollection<ActivityTarget> ActivityTargets { get; set; }
    }
}
