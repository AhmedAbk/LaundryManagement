using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LaundryDTO> Laundries { get; set; }

        public OwnerDTO()
        {
            Laundries = new List<LaundryDTO>();
        }
    }
}
