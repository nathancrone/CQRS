﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.AWS.Activity1Console.Shared
{
    class ActivityTaskCompletedResult
    {
        public WorkflowExecutionStartedInput StartingInput { get; set; }
        public int RequestActionId { get; set; }
    }
}
