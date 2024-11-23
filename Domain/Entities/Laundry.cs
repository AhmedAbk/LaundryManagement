using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Laundry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public List<Machine> Machines { get; set; }

        public Laundry()
        {
            Machines = new List<Machine>();
        }
    }
}
