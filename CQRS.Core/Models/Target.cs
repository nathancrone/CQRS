using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Target
    {
        public int? TargetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ActionTarget> ActionTargets { get; set; }
        public ICollection<ActivityTarget> ActivityTargets { get; set; }

    }
}
