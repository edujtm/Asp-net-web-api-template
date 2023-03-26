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
        public async Task<IActionResult> GetAll(Guid customerId)
        {
            return Ok(await _serviceManager.BookingService.GetAllAsync(customerId, trackChanges: false));
        }

        [HttpGet("{id:guid}", Name = "GetBookingByCustomerId")]
        public async Task<IActionResult> GetById(Guid customerId, Guid Id)
        {
            return Ok(await _serviceManager.BookingService.GetByIdAsync(customerId, Id, trackChanges: false));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid customerId, [FromBody] BookingCreationDto bookingCreationDto)
        {
            if (bookingCreationDto == null) { return BadRequest("Booking object mustn't be null."); }

            var result = await _serviceManager.BookingService.CreateAsync(customerId, bookingCreationDto, false);

            return CreatedAtRoute("GetBookingByCustomerId", new { customerId, id = result.Id }, result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid customerId, Guid Id)
        {
            await _serviceManager.BookingService.DeleteBookingAsync(customerId, Id, false);
            return NoContent();
        }
    }

}
