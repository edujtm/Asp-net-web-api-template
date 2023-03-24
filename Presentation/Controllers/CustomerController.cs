using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(_serviceManager.CustomerService.GetById(Id, trackChanges: false));
        }
    }
}
