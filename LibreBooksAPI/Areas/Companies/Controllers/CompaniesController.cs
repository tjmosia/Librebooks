using LibreBooks.Areas.Companies.Models;
using LibreBooks.Extensions.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibreBooks.Areas.Companies.Controllers
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
        public IActionResult CreateAsync ([FromBody] CompaniesReqModels input)
        {
            return Ok();
        }
    }
}
