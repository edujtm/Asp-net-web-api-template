using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [Route("api/customers/{customerId}/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BookingController(IServiceManager service) => _serviceManager = service;

        [HttpGet]
        public IActionResult GetAll(Guid customerId)
        {
            return Ok(_serviceManager.BookingService.GetAll(customerId, trackChanges: false));
        }

        [HttpGet("{id:guid}", Name = "GetBookingByCustomerId")]
        public IActionResult GetById(Guid customerId, Guid Id)
        {
            return Ok(_serviceManager.BookingService.GetById(customerId, Id, trackChanges: false));
        }

        [HttpPost("vehicles/{vehicleId:guid}")]
        public IActionResult Create(Guid customerId, Guid VehicleId, [FromBody] BookingCreationDto bookingCreationDto)
        {
            if (bookingCreationDto == null) { return BadRequest("Booking object mustn't be null."); }

            var result = _serviceManager.BookingService.Create(customerId, VehicleId, bookingCreationDto, false);

            return CreatedAtRoute("GetBookingByCustomerId", new { customerId, id = result.Id }, result);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid customerId, Guid Id) 
        {
            _serviceManager.BookingService.DeleteBooking(customerId, Id, false);
            return NoContent();
        }
    }

}
