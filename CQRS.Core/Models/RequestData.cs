using System;
using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class RequestData
    {
        public int? RequestDataId { get; set; }
        public int? RequestId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual Request Request { get; set; }
    }
}
