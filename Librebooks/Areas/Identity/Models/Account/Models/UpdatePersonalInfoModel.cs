using FluentValidation;
using FluentValidation.Results;

namespace Librebooks.Areas.Identity.Models.Account.Models;

public class UpdatePersonalInfoModel
{
	public class Request
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Gender { get; set; }
	}

	public static ValidationResult Validate (Request request)
		=> new Validator().Validate(request);

	private class Validator : AbstractValidator<Request>
	{
		public Validator ()
		{
			RuleFor(p => p.FirstName)
				.NotEmpty().WithMessage("First name is required.");

			RuleFor(p => p.LastName)
				.NotEmpty().WithMessage("Last name is required.");

			RuleFor(p => p.Gender)
				.NotEmpty().WithMessage("Gender is required.");
		}
	}
}