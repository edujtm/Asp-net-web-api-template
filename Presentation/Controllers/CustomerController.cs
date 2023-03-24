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
            try
            {
                return Ok(_serviceManager.CustomerService.GetAll(trackChanges: false));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }
    }
}
