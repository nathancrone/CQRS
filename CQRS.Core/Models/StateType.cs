﻿using System.Collections.Generic;

namespace CQRS.Core.Models
{
    public class StateType
    {
        public int? StateTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<State> States { get; set; }
    }
}
