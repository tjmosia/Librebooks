using System.ComponentModel.DataAnnotations;
using LibrebooksRazor.Providers.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrebooksRazor.Areas.Identity.Pages.Auth;

public class RegisterModel (IVerificationManager verificationManager, ILogger<RegisterModel> logger) : PageModel
{
	private readonly IVerificationManager verificationManager = verificationManager;
	private readonly ILogger<RegisterModel> logger = logger;

	[BindProperty]
	public RegisterInputModel? InputModel { get; set; }

	public IActionResult OnGet ()
	{
		if (HttpContext.Session.GetString(AuthSessionKeys.Email) is null)
			return RedirectToPage("./UsernameEntry");

		return Page();
	}

	public IActionResult OnPostChangeEmail ()
	{
		return RedirectToPage("./UsernameEntry");
	}

	public async Task<IActionResult> OnPostRegister ()
	{
		var email = HttpContext.Session.GetString(AuthSessionKeys.Email);

		if (string.IsNullOrEmpty(email))
			return RedirectToPage("./UsernameEntry");

		if (!ModelState.IsValid)
			return Page();

		var verificationResult = await verificationManager.VerifyAsync(email, AuthEmailVerificationReasons.Registration, InputModel!.Code!);

		if (verified.Succeeded)

			return RedirectToPage("./Register");
	}

	public async Task<IActionResult> OnPostResendVerificationCodeAsync ()
	{
		var email = HttpContext.Session.GetString(AuthSessionKeys.Email);

		if (string.IsNullOrEmpty(email))
			return RedirectToPage("./UsernameEntry");

		if (!ModelState.IsValid)
		{
			var erred = false;

			foreach (var modelState in ModelState)
			{

				if (modelState.Key != "InputModel.Code")
				{
					ModelState.Remove(modelState.Key);
				}
				else
				{
					erred = true;
				}
			}
			if (erred)
			{
				return Page();
			}
		}

		var request = await AuthFunctions.SendVerificationEmailAsync(verificationManager, email, AuthEmailVerificationReasons.Registration);
		logger.LogInformation("User email verification sent to {email} with code {code}", email, request.code);

		return Page();
	}

	public class RegisterInputModel
	{
		[Display(Name = "First name")]
		[Required(ErrorMessage = "First name is required.")]
		public string? FirstName { get; set; }

		[Display(Name = "Last name")]
		[Required(ErrorMessage = "Last name is required.")]
		public string? LastName { get; set; }

		[Display(Name = "Password")]
		[Required(ErrorMessage = "Password is required.")]
		public string? Password { get; set; }

		[Display(Name = "Verification Code")]
		[Required(ErrorMessage = "Verification code is required.")]
		public string? Code { get; set; }

		[Display(Name = "Confirm password")]
		[Required(ErrorMessage = "Confirm password is required.")]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string? ConfirmPassword { get; set; }
	}
}
