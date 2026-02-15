using FluentValidation;
using FluentValidation.Results;

namespace Librebooks.Areas.Identity.Models.Authentication.Models
{
	public class RegisterModel
	{
		public class Request
		{
			public string? Email { get; set; }
			public string? FirstName { get; set; }
			public string? LastName { get; set; }
			public string? Gender { get; set; }
			public string? Password { get; set; }
			public string? Code { get; set; }
		}

		public static ValidationResult Validate (Request request)
			=> new Validator().Validate(request);

		private class Validator : AbstractValidator<Request>
		{
			public Validator ()
			{
				RuleFor(p => p.Email)
					.Cascade(CascadeMode.Stop)
					.NotEmpty().WithMessage("Email is required.")
					.EmailAddress().WithMessage("Please check your email.");

				RuleFor(p => p.FirstName)
					.NotEmpty().WithMessage("First name is required.");

				RuleFor(p => p.LastName)
					.NotEmpty().WithMessage("Last name is required.");

				RuleFor(p => p.Gender)
					.NotEmpty().WithMessage("Gender is required.");

				RuleFor(p => p.Password)
					.NotEmpty().WithMessage("Gender is required.");

				RuleFor(p => p.Code)
					.NotEmpty().WithMessage("Code is required.");

			}
		}
	}
}
