using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaundriesController : ControllerBase
    {
        private readonly ILaundryService _laundryService;
        private readonly ILogger<LaundriesController> _logger;

        public LaundriesController(ILaundryService laundryService, ILogger<LaundriesController> logger)
        {
            _laundryService = laundryService;
            _logger = logger;
        }

        [HttpGet("{id}/machines")]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetLaundryMachines(int id)
        {
            try
            {
                var machines = await _laundryService.GetMachinesByLaundryIdAsync(id);
                return Ok(machines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting machines for laundry {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

