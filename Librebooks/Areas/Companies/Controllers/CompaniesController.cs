using Librebooks.Areas.Admin.Services;
using Librebooks.Areas.Companies.Data;
using Librebooks.Areas.Companies.Models;
using Librebooks.Areas.Companies.Services;
using Librebooks.Areas.Identity.Services;
using Librebooks.Extensions.Mvc;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Companies.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CompaniesController
	(ISystemManager sysManager,
	ICompanyManager companyManager,
	UserManagerExtension userManager,
	ICompanyStore companyStore)
	: SessionControllerBase(userManager)
{
	private readonly ISystemManager sysManager = sysManager;
	private readonly ICompanyManager companyManager = companyManager;
	private readonly ICompanyStore companyStore = companyStore;

	[HttpGet]
	[Route("")]
	public async Task<IActionResult> GetAsync ()
	{
		var user = await userManager!.FindByNameAsync(User.Identity!.Name!);

		if (user == null)
			return Unauthorized();

		var company = await companyStore.FindByUserIdAsync(user!.Id);

		if (company == null)
			return NotFound();

		return Ok(new CompanyDto(company));
	}

	[HttpPost]
	[Route("create")]
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
			EmailAddress = input.Email
		};

		return Ok();
	}

	private static void SetCompanyToSession (HttpContext context, int companyId)
	{
		context.Response.Cookies.Append(nameof(companyId), companyId.ToString());
	}
}
