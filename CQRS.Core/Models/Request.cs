using System;
using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class Request
    {
        public int? RequestId { get; set; }
        public int? ProcessId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public DateTime DateRequested { get; set; }

        public virtual Process Process { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<User> Stakeholders { get; set; }
        public virtual ICollection<RequestNote> RequestNotes { get; set; }
        public virtual ICollection<RequestData> RequestData { get; set; }
    }
}
