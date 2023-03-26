using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs;
using Shared.RequestHelper;
using System.Text.Json;

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
        public async Task<IActionResult> GetAll([FromQuery] CustomerParams customerParams)
        {
            var pagedResult = await _serviceManager.CustomerService.GetAllAsync(customerParams, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaDataRequest));

            return Ok(pagedResult.customerDtos);
        }

        [HttpGet("{id:guid}", Name = "CustomerById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _serviceManager.CustomerService.GetByIdAsync(Id, trackChanges: false));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create([FromBody] CustomerCreationDto customerCreationDto)
        {
            var createdCustomer = await _serviceManager.CustomerService.CreateAsync(customerCreationDto);
            return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await _serviceManager.CustomerService.DeleteCustomerAsync(Id, false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerUpdateDto customerDto)
        {
            await _serviceManager.CustomerService.UpdateCustomerAsync(id, customerDto, trackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<CustomerUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _serviceManager.CustomerService.GetCustomerForPatchAsync(id, trackChanges: true);

            patchDoc.ApplyTo(result.customerPatchDto, ModelState);

            TryValidateModel(result.customerPatchDto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _serviceManager.CustomerService.PatchAsync(result.customerPatchDto, result.customer);

            return NoContent();
        }

    }
}
