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
        public int? CurrentStateId { get; set; }

        public Process Process { get; set; }
        public User User { get; set; }

        public ICollection<User> Stakeholders { get; set; }
        public ICollection<RequestNote> RequestNotes { get; set; }
        public ICollection<RequestData> RequestData { get; set; }
        public ICollection<RequestAction> RequestActions { get; set; }
    }
}
