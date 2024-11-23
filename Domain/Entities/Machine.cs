using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int LaundryId { get; set; }
        public List<Cycle> Cycles { get; set; }

        public Machine()
        {
            Cycles = new List<Cycle>();
        }
    }

}
