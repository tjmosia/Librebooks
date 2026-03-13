using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrebooksRazor.Areas.Identity.Pages.Auth;

public class RegisterModel : PageModel
{
	public RegisterModel ()
	{
		InputModel = new();
	}

	[BindProperty]
	public RegisterInputModel InputModel { get; set; }

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

	public IActionResult OnPostRegister ()
	{
		if (!ModelState.IsValid)
			return Page();

		return RedirectToPage("./Register");
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
