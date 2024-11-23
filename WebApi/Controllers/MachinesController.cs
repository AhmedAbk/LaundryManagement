using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly ILaundryService _laundryService;
        private readonly ILogger<MachinesController> _logger;

        public MachinesController(ILaundryService laundryService, ILogger<MachinesController> logger)
        {
            _laundryService = laundryService;
            _logger = logger;
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateMachineStatus(int id, [FromBody] UpdateMachineStatusRequest request)
        {
            try
            {
                var success = await _laundryService.UpdateMachineStatusAsync(id, request.Status);
                if (success)
                    return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for machine {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
