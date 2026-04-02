using Librebooks.Areas.Systems.Data;
using Librebooks.Areas.Systems.Models;
using Librebooks.Areas.Systems.Services;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Systems.Controllers;

[ApiController]
public class TaxesController (ISystemsManager systemManager)
	: SystemsControllerBase(systemManager)
{
	[Route(""), HttpGet]
	public async Task<IList<TaxData>> GetAsync ()
	{
		return [.. (await Manager.GetTaxesAsync())
			.Select(p => new TaxData(p))];
	}

	[Route("{id}"), HttpGet]
	public async Task<IActionResult> GetAsync (int id)
	{
		var tax = await Manager.FindTaxByIdAsync(id);
		if (tax == null)
			return NotFound();
		return Ok(new TaxData(tax));
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync ([FromBody] TaxAddModel.Request model)
	{
		var modelState = TaxAddModel.Validate(model);

		if (!modelState.IsValid)
			return BadRequest(Result.Failure([.. modelState.Errors.Select(p => Error.Create(p.PropertyName, p.ErrorMessage))]));

		var tax = new Tax
		{
			Rate = model.Rate,
			Name = model.Name,
			System = model.System,
			Type = model.Type
		};

		var result = await Manager.AddTaxAsync(tax);

		if (result.Succeeded)
			return Ok(Result<TaxData>.Success(new TaxData(result.Model!)));

		return Ok(result);
	}

	[HttpPatch("{id}")]
	public async Task<IActionResult> UpdateAsync ([FromBody] TaxUpdateModel.Request model, int id)
	{
		var modelState = TaxAddModel.Validate(model);

		if (!modelState.IsValid)
			return BadRequest(Result.Failure([.. modelState.Errors.Select(p => Error.Create(p.PropertyName, p.ErrorMessage))]));

		var tax = await Manager.FindTaxByIdAsync(id);

		if (tax == null)
			return NotFound();

		if (tax.Rate == model.Rate && tax.Name == model.Name && tax.Type == model.Type && tax.System == model.System)
			return Ok(Result<TaxData>.Success(new TaxData(tax)));

		tax.Type = model.Type;
		tax.Rate = model.Rate;
		tax.System = model.System;
		tax.Name = model.Name;

		var result = await Manager.UpdateTaxAsync(tax);

		if (result.Succeeded)
			return Ok(Result<TaxData>.Success(new TaxData(result.Model!)));

		return Ok(result);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAsync (int id)
	{
		var tax = await Manager.FindTaxByIdAsync(id);

		if (tax == null)
			return NotFound();

		var result = await Manager.DeleteTaxesAsync(tax);
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAsync ([FromBody] int[] ids)
	{
		var taxes = await Manager.GetTaxesAsync();
		var taxesToDelete = new List<Tax>();

		foreach (var id in ids)
			foreach (var tax in taxes)
				if (id == tax.Id)
					taxesToDelete.Add(tax);

		if (taxesToDelete.Count == 0)
			return Ok(Result.Success);

		var result = await Manager.DeleteTaxesAsync([.. taxesToDelete]);
		return Ok(result);
	}
}
