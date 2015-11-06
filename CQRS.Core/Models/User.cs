using System;
using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class User
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Process> AdministeredProcesses { get; set; }
        public virtual ICollection<Request> StakeRequests { get; set; }
        public virtual ICollection<RequestNote> RequestNotes { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
