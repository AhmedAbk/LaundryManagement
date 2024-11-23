using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class LaundryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public List<MachineDTO> Machines { get; set; }

        public LaundryDTO()
        {
            Machines = new List<MachineDTO>();
        }
    }
}
