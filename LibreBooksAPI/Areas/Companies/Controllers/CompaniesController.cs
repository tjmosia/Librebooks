using LibreBooks.Areas.Companies.Models;
using LibreBooks.Areas.Companies.Services;
using LibreBooks.Areas.SystemSetups.Services;
using LibreBooks.Extensions.Identity;
using LibreBooks.Extensions.Mvc;
using LibreBooks.Models.Entity.CompanySpace;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibreBooks.Areas.Companies.Controllers
{
    [Authorize]
    [ApiController]
    [Route("companies")]
    public class CompaniesController (SystemManager sysManager, CompanyManager companyManager, UserManagerExt userManager) : SessionControllerBase(userManager)
    {
        private readonly SystemManager sysManager = sysManager;
        private readonly CompanyManager companyManager = companyManager;

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAsync ()
        {
            var user = await userManager!.FindByNameAsync(User.Identity!.Name!);

            if (user == null)
                return Unauthorized();

            return Ok(await companyManager.FindAllByUserAsync(user!.Id));
        }

        [HttpPost]
        [Route("/companies/create")]
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
