using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class ActivityTarget
    {
        public int? ActivityTargetId { get; set; }
        public int? ActivityId { get; set; }
        public int? TargetId { get; set; }
        public int? GroupId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Target Target { get; set; }
        public virtual Group Group { get; set; }
    }
}
