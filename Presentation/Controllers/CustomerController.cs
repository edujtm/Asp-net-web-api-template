using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.Extensions;
using Service.Contracts;
using Shared.DTOs;
using Shared.RequestHelper;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/customers")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CustomerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Gets the list of all customers
        /// </summary>
        /// <param name="customerParams"></param>
        /// <returns>The customers list</returns>
        [HttpGet]
        /// <summary> Create a customer </summary>
        /// <param name="CustomerParams"></param>
        /// <remarks>
        /// Descrição mais longa >>> Lorem Ipsum é simplesmente uma simulação de texto 
        /// da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI,
        /// quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro d
        /// e modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a edito
        /// ração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Le
        /// traset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser in
        /// tegrado a softwares de editoração eletrônica como Aldus PageMaker.
        /// </remarks> 
        /// <returns>Customer created</returns>
        [ProducesResponseType(typeof(List<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll([FromQuery] CustomerParams customerParams)
        {
            var pagedResult = await _serviceManager.CustomerService.GetAllAsync(customerParams, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaDataRequest));

            return Ok(pagedResult.customerDtos.ShapeData(customerParams.Properties));
        }

        [HttpGet("{id:guid}", Name = "CustomerById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _serviceManager.CustomerService.GetByIdAsync(Id, trackChanges: false));
        }

        /// <summary> Create a customer </summary>
        /// <param name="customerCreationDto"></param>
        /// <remarks>
        /// Descrição mais longa >>> Lorem Ipsum é simplesmente uma simulação de texto 
        /// da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI,
        /// quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro d
        /// e modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a edito
        /// ração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Le
        /// traset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser in
        /// tegrado a softwares de editoração eletrônica como Aldus PageMaker.
        /// </remarks> 
        /// <returns>Customer created</returns>
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
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
