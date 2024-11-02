using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Moskit.Areas.Companies.Models;

namespace Moskit.Areas.Companies.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get ([FromRoute] string id)
        {


            return Ok();
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult CreateAsync ([FromBody] CompanyInputModel input)
        {
            return Ok();
        }
    }
}
