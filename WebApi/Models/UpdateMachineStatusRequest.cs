using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UpdateMachineStatusRequest
    {

        [Required]
        public string Status { get; set; }
    }
}
