using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class ActivityTarget
    {
        public int? ActivityTargetId { get; set; }
        public int? ActivityId { get; set; }
        public int? TargetId { get; set; }
        public int? GroupId { get; set; }

        public Activity Activity { get; set; }
        public Target Target { get; set; }
        public Group Group { get; set; }
    }
}
