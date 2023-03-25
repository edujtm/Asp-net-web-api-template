using Microsoft.AspNetCore.JsonPatch;
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

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdCustomer = _serviceManager.CustomerService.Create(customerCreationDto);
            return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid Id)
        {
            _serviceManager.CustomerService.DeleteCustomer(Id, false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] CustomerUpdateDto customerDto)
        {
            if (customerDto is null)
                return BadRequest("Object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.CustomerService.UpdateCustomer(id, customerDto, trackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public IActionResult Patch(Guid id, [FromBody] JsonPatchDocument<CustomerUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = _serviceManager.CustomerService.GetCustomerForPatch(id, trackChanges: true);

            patchDoc.ApplyTo(result.customerPatchDto, ModelState);

            TryValidateModel(result.customerPatchDto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.CustomerService.Patch(result.customerPatchDto, result.customer);

            return NoContent();
        }

    }
}
