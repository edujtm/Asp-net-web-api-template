using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public VehicleController(IServiceManager serviceManager) =>
            _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _serviceManager.VehicleService.GetAllAsync(trackChanges: false));
        }

        [HttpGet("{id:guid}", Name = "GetVehicleById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _serviceManager.VehicleService.GetByIdAsync(Id, trackChanges: false));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleCreationDto vehicleCreationDto)
        {
            if (vehicleCreationDto is null)
                return BadRequest("Vehicle object is null.");

            var createdVehicle = await _serviceManager.VehicleService.CreateAsync(vehicleCreationDto);
            return CreatedAtRoute("GetVehicleById", new { id = createdVehicle.Id }, createdVehicle);
        }
    }
}
