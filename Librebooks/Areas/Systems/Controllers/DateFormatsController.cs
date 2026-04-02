using Librebooks.Areas.Systems.Data;
using Librebooks.Areas.Systems.Models;
using Librebooks.Areas.Systems.Services;
using Librebooks.CoreLib.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Librebooks.Areas.Systems.Controllers;

[ApiController]
public class DateFormatsController (ISystemsManager systemManager)
	: SystemsControllerBase(systemManager)
{
	[HttpGet]
	public async Task<IList<DateFormatData>> OnGetAsync ()
	{
		return [.. (await Manager.GetDateFormatsAsync()).Select(p => new DateFormatData(p))];
	}

	[HttpPost]
	public async Task<IActionResult> OnPostAsync ([FromBody] DateFormatsAddModels.Request model)
	{
		var modelState = DateFormatsAddModels.Validate(model);

		if (!modelState.IsValid)
			return BadRequest(Result.Failure([..modelState.Errors
				.Select( p=> Error.Create(p.PropertyName, p.ErrorMessage))]));

		var result = Manager.AddDateFormatAsync(new()
		{
			Format = model.Format,
		});

		return Ok(result);
	}

	[HttpPost("{id}")]
	public async Task<IActionResult> OnPatchAsync (int id, [FromBody] DateFormatsAddModels.Request model, CancellationToken cancellationToken)
	{
		var modelState = DateFormatsAddModels.Validate(model);

		if (!modelState.IsValid)
			return BadRequest(Result.Failure([..modelState.Errors
				.Select( p=> Error.Create(p.PropertyName, p.ErrorMessage))]));

		var format = Manager.FindDateFormatByIdAsync(id, cancellationToken);

		var result = Manager.AddDateFormatAsync(new()
		{
			Format = model.Format,
		});

		return Ok(result);
	}
}
