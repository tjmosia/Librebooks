using Librebooks.Areas.Admin.Data;
using Librebooks.Areas.Admin.Models;
using Librebooks.Areas.Admin.Services;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Admin.Controllers;

[Route("countries")]
[ApiController]
public class CountriesController (ISystemManager systemManager, SystemStore sysStore, ILogger<CountriesController> logger)
	: ControllerBase
{
	private readonly ISystemManager sysManager = systemManager;
	private readonly SystemStore sysStore = sysStore;
	private readonly ILogger<CountriesController> logger = logger;

	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> CreateCountryAsync ([FromBody] CountryModel.Create.Request model)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var country = model.MapToCountry(new Country());

		country.NormalizeCode();

		var result = await sysStore.Countries.AddAsync(country);

		return BadRequest(result);
	}

	[HttpPatch]
	[Route("edit/{countryId}")]
	public async Task<IActionResult> UpdateCountryAsync ([FromBody] CountryModel.Update.Request model, [FromRoute] int countryId)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var country = await sysStore.Countries.FindByIdAsync(countryId);

		if (country == null)
			return NotFound();

		if (country.Name!.Equals(model.Name, StringComparison.CurrentCultureIgnoreCase) &&
			country.DialingCode!.Equals(model.DialingCode, StringComparison.CurrentCultureIgnoreCase) &&
			country.Code!.Equals(model.Code, StringComparison.CurrentCultureIgnoreCase))
		{
			return Ok(TransactionResult<CountryDTO>.Success(new CountryDTO(country)));
		}
		var result = await sysStore.Countries.UpdateAsync(model.MapToCountry(country));

		if (result.Succeeded)
			return Ok(TransactionResult<CountryDTO>.Success(new CountryDTO(result.Model!)));
		else
			return Ok(TransactionResult.Failure(result.Errors));
	}

	[HttpGet]
	[Route("{countryId}")]
	public async Task<IActionResult> GetCountryAsync ([FromRoute] int countryId)
	{
		var country = await sysStore.Countries.FindByIdAsync(countryId);
		if (country == null)
			return NotFound();

		return Ok(new CountryDTO(country));
	}

	[HttpGet]
	[Route("")]
	public async Task<IEnumerable<CountryDTO>> GetCountriesAsync (CancellationToken cancellationToken = default)
	{
		var countries = await sysStore.Countries.FindAllAsync(cancellationToken);
		return countries.Any() ? countries.Select(p => new CountryDTO(p)).ToArray() : [];
	}

	[HttpDelete]
	[Route("delete")]
	public async Task<IActionResult> DeleteCountriesAsync ([FromBody] int[] countryIds)
	{
		var countries = await sysStore.Countries.FindAllByIdAsync(countryIds);

		if (!countries.Any())
			return Ok(TransactionResult.Success);

		var result = await sysStore.Countries.DeleteAsync([.. countries]);

		if (result.Succeeded)
			return Ok(TransactionResult.Success);
		else
			return Ok(TransactionResult.Failure(TransactionError.Create("", "Unable to delete countries that are in use.")));
	}

	[HttpDelete]
	[Route("delete/{id}")]
	public async Task<IActionResult> DeleteCountryAsync ([FromRoute] int id)
	{
		var country = await sysStore.Countries.FindByIdAsync(id);

		if (country == null)
			return NotFound();

		var result = await sysStore.Countries.DeleteAsync(country!);

		if (result.Succeeded)
			return Ok(TransactionResult.Success);
		else
			return Ok(TransactionResult.Failure(TransactionError.Create("", "Country is currently in use.")));
	}
}
