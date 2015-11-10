using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class ActivityType
    {
        public int? ActivityTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
