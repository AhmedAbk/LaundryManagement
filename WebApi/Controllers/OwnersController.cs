using Microsoft.AspNetCore.Mvc;
using Business.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
 
using Microsoft.Extensions.Logging;
using Business.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly ILaundryService _laundryService;
        private readonly ILogger<OwnersController> _logger;

        public OwnersController(ILaundryService laundryService, ILogger<OwnersController> logger)
        {
            _laundryService = laundryService ?? throw new ArgumentNullException(nameof(laundryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDTO>>> GetAllOwners()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all owners");
                var owners = await _laundryService.GetAllOwnersAsync();

                if (owners == null)
                {
                    _logger.LogWarning("No owners found");
                    return NotFound("No owners found");
                }

                return Ok(owners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all owners: {ErrorMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}