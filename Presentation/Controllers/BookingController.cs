using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid customerId, Guid Id)
        {
            return Ok(_serviceManager.BookingService.GetById(customerId, Id, trackChanges: false));
        }
    }

}
