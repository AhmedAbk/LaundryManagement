using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int LaundryId { get; set; }
        public List<CycleDTO> Cycles { get; set; }

        public MachineDTO()
        {
            Cycles = new List<CycleDTO>();
        }
    }
}
