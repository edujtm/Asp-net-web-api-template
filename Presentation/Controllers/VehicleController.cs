using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public VehicleController(IServiceManager serviceManager) =>
            _serviceManager = serviceManager;

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _serviceManager.VehicleService.GetAllAsync(trackChanges: false));
        }

        [HttpGet("{id:guid}", Name = "GetVehicleById")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _serviceManager.VehicleService.GetByIdAsync(Id, trackChanges: false));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create([FromBody] VehicleCreationDto vehicleCreationDto)
        {
            var createdVehicle = await _serviceManager.VehicleService.CreateAsync(vehicleCreationDto);
            return CreatedAtRoute("GetVehicleById", new { id = createdVehicle.Id }, createdVehicle);
        }
    }
}
