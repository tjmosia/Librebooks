using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OskitAPI.Areas.Companies.Models;
using OskitAPI.Extensions.Mvc;

namespace OskitAPI.Areas.Companies.Controllers
{
    [Authorize]
    [ApiController]
    [Route("companies")]
    public class CompaniesController : SessionControllerBase
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
