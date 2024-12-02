using LibreBooks.Areas.SystemSetups.Services;

using Microsoft.AspNetCore.Mvc;

namespace LibreBooksAPI.Areas.SystemSetups.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("sectors")]
    public class BusinessSectorsController (ISystemManager systemManager)
        : ControllerBase
    {
        private readonly ISystemManager systemManager = systemManager;

        [Route("create")]
        [HttpPost]
        public IActionResult CreateAsync ()
        {
            return Ok();
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetASync ()
        {
            var sectors = await systemManager.GetBusinessSectorsAsync();
            return Ok(sectors.Count > 0 ? sectors.Select(p => new { p.Name, p.Id }).ToArray() : []);
        }
    }
}
