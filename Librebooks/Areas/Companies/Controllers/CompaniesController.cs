using Librebooks.Areas.Admin.Services;
using Librebooks.Areas.Companies.Data;
using Librebooks.Areas.Companies.Models;
using Librebooks.Areas.Companies.Services;
using Librebooks.Areas.Identity.Services;
using Librebooks.CoreLib.Operations;
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
	[Route("{id}")]
	public async Task<IActionResult> GetAsync ([FromRoute] int id)
	{
		var user = await userManager!.FindByNameAsync(User.Identity!.Name!);

		if (user == null)
			return Unauthorized();

		var company = await companyStore.FindByIdAsync(id, user.Id);

		return company == null ? NotFound() : Ok(new CompanyDto(company));
	}

	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> CreateAsync ([FromBody] CompaniesModels.Request input)
	{
		var validationResult = CompaniesModels.Validate(input);

		if (!validationResult.IsValid)
			return BadRequest(TransactionResult.Failure([.. validationResult.Errors.Select(p => TransactionError.Create(p.PropertyName, p.ErrorMessage))]));

		var company = new Company()
		{
			BusinessSectorId = input.BusinessSector,
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
