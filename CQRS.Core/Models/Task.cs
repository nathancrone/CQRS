using System;

namespace CQRS.Core.Models
{
    public class Task
    {
        public int? TaskId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

