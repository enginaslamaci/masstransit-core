﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class CompleteProcessEvent
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
    }
}
