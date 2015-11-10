using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class ActionTarget
    {
        public int? ActionTargetId { get; set; }
        public int? ActionId { get; set; }
        public int? TargetId { get; set; }
        public int? GroupId { get; set; }

        public Action Action { get; set; }
        public Target Target { get; set; }
        public Group Group { get; set; }
    }
}
