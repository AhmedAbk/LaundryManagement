using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Laundry> Laundries { get; set; }

        public Owner()
        {
            Laundries = new List<Laundry>();
        }
    }
}
