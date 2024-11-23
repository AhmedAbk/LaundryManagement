﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cycle
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public decimal Income { get; set; }
        public int MachineId { get; set; }
    }
}
