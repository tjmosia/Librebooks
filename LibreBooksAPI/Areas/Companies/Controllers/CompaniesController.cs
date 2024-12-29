using LibreBooks.Areas.Companies.Models;
using LibreBooks.Areas.SystemSetups.Services;
using LibreBooks.Extensions.Mvc;
using LibreBooks.Models.Entity.CompanySpace;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibreBooks.Areas.Companies.Controllers
{
    [Authorize]
    [ApiController]
    [Route("companies")]
    public class CompaniesController (SystemManager sysManager) : SessionControllerBase
    {
        private readonly SystemManager sysManager = sysManager;

        [HttpGet]
        [Route("")]
        public IActionResult Get ([FromRoute] string id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> CreateAsync ([FromBody] CompaniesReqModels.CreateModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var businessSector = await sysManager.FindBusinessSectorByIdAsync(input.BusinessSector!);

            if (businessSector == null)
            {
                ModelState.AddModelError(nameof(input.BusinessSector), "Invalid business sector provided.");
                return BadRequest(ModelState);
            }

            var company = new Company()
            {
                BusinessSectorId = businessSector.Id,
                RegNumber = input.RegNumber,
                FaxNumber = input.FaxNumber,
                PhysicalAddress = input.PhysicalAddress,
                PostalAddress = input.PostalAddress,
                PhoneNumber = input.TelephoneNumber,
                EmailAddress = input.EmailAddress
            };


            return Ok();
        }
    }
}
