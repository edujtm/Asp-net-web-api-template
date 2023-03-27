using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.RequestHelper;

namespace Presentation.Controllers
{
    [ApiVersion("0.0", Deprecated = true)]
    [Route("api/customers")]
    [ApiController]
    public class V2CustomerController : ControllerBase
    { 
        [HttpGet]
        public ActionResult GetAll([FromQuery] CustomerParams customerParams)
        {
            return Ok("Customer from APi V0");
        }
    }
}
