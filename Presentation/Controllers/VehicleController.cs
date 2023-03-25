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
        public IActionResult GetAll()
        {
            return Ok(_serviceManager.VehicleService.GetAll(trackChanges: false));
        }

        [HttpGet("{id:guid}", Name = "GetVehicleById")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(_serviceManager.VehicleService.GetById(Id, trackChanges: false));
        }

        [HttpPost]
        public IActionResult Create([FromBody] VehicleCreationDto vehicleCreationDto)
        {
            if (vehicleCreationDto is null)
                return BadRequest("Vehicle object is null.");

            var createdVehicle = _serviceManager.VehicleService.Create(vehicleCreationDto);
            return CreatedAtRoute("GetVehicleById", new { id = createdVehicle.Id }, createdVehicle);
        }
    }
}
