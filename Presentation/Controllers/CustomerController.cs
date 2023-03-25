using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CustomerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_serviceManager.CustomerService.GetAll(trackChanges: false));
        }

        [HttpGet("{id:guid}", Name = "CustomerById")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(_serviceManager.CustomerService.GetById(Id, trackChanges: false));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerCreationDto customerCreationDto)
        {
            if (customerCreationDto is null)
                return BadRequest("CompanyForCreationDto object is null");
            var createdCustomer = _serviceManager.CustomerService.Create(customerCreationDto);
            return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid Id)
        {
            _serviceManager.CustomerService.DeleteCustomer(Id, false);
            return NoContent();
        }

    }
}
