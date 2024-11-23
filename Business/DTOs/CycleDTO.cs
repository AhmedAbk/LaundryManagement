using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CycleDTO
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public decimal Income { get; set; }
        public int MachineId { get; set; }
    }
}
