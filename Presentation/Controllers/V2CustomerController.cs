using Microsoft.AspNetCore.Mvc;
using Shared.RequestHelper;

namespace Presentation.Controllers
{
    [ApiVersion("0.0", Deprecated = true)]
    [Route("api/customers")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]

    public class V2CustomerController : ControllerBase
    { 
        [HttpGet]
        public ActionResult GetAll([FromQuery] CustomerParams customerParams)
        {
            return Ok("Customer from APi V0");
        }
    }
}
