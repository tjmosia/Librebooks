using System.Text;
using Librebooks.Areas.Companies.Data;
using Librebooks.Areas.Companies.Models;
using Librebooks.Areas.Companies.Services;
using Librebooks.Areas.Identity.Services;
using Librebooks.Areas.Systems.Services;
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
	(ISystemsManager sysManager,
	ICompanyManager companyManager,
	UserManagerExtension userManager,
	ICompanyStore companyStore)
	: SessionControllerBase(userManager)
{
	private readonly ISystemsManager sysManager = sysManager;
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

		return company == null ? NotFound() : Ok(new CompanySummaryData(company));
	}

	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> CreateAsync ([FromBody] CompaniesModels.Request input)
	{
		var validationResult = CompaniesModels.Validate(input);

		if (!validationResult.IsValid)
			return BadRequest(Result.Failure([.. validationResult.Errors.Select(p => Error.Create(p.PropertyName, p.ErrorMessage))]));

		var taxes = await sysManager.GetTaxesAsync();

		var company = new Company()
		{
			BusinessSectorId = input.BusinessSectorId,
			RegNumber = input.RegNumber,
			FaxNumber = input.FaxNumber,
			PhysicalAddress = input.PhysicalAddress,
			PostalAddress = input.PostalAddress,
			PhoneNumber = input.TelephoneNumber,
			EmailAddress = input.Email,
			VATNumber = input.VATNumber,
			TradingName = input.TradingName,
			LegalName = input.LegalName,
			RegionalSetup = new()
			{
				CurrencyId = input.CurrencyId,
				CountryId = input.CountryId,
				DateFormatId = input.DateFormatId,
				DecimalMark = ".",
				RoundToNearest = 2,
				ThousandsSeperator = " "
			},
			Taxes = [..taxes.Select(p => new CompanyTax
			{
				TaxType = p,
			})],


		};

		if (input.Logo != null)
		{
			company.Logo = new CompanyLogo
			{
				Image = new CompanyImage
				{
					Data = Encoding.UTF8.GetBytes(input.Logo)
				}
			};
		}

		var result = await companyStore.CreateAsync(company);

		if (result.Succeeded)
		{
			return Created();
		}
		else
		{
			return Ok(Result.Failure([.. result.Errors.Select(p => Error.Create(p.Code, p.Description))]));
		}
	}
}
