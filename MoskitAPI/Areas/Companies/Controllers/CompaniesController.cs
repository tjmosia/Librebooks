using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moskit.Areas.Companies.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get ([FromRoute] string id)
        {


            return Ok();
        }
    }
}
