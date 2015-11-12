﻿using System;
using System.Collections.Generic;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Service.QueryResults
{
    public class TransitionByCurrentAndNextStateQueryResult : IQueryResult
    {
        public Transition Transition { get; set; }
    }
}