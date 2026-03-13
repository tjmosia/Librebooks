using System.ComponentModel.DataAnnotations;
using LibrebooksRazor.Extensions.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrebooksRazor.Areas.Identity.Pages.Auth;

public class UsernameEntryModel (UserManagerExtension userManager) : PageModel
{
	private readonly UserManagerExtension userManager = userManager;

	[BindProperty]
	[Required(ErrorMessage = "Email is required."), EmailAddress(ErrorMessage = "Email is invalid.")]
	public string? Email { get; set; }

	[TempData]
	public string? ErrorMessage { get; set; }

	[FromQuery(Name = "returnUrl")]
	public string? ReturnUrl { get; set; }

	public IActionResult OnGet ()
	{
		var emailFromSession = HttpContext.Session.GetString(AuthSessionKeys.Email);

		ReturnUrl ??= Url.Content("~/");

		if (emailFromSession is not null)
			Email = emailFromSession;

		HttpContext.Session.Clear();

		return Page();
	}

	public async Task<IActionResult> OnPostAsync ()
	{
		if (!ModelState.IsValid)
			return Page();

		ReturnUrl ??= Url.Content("~/");

		var user = await userManager.FindByEmailAsync(Email!);

		HttpContext.Session.SetString(AuthSessionKeys.Email, Email!);

		if (user == null)
			return RedirectToPage("./Register");

		HttpContext.Session.SetString(AuthSessionKeys.GivenName, user.FirstName!);

		if (user.Photo is not null)
			HttpContext.Session.SetString(AuthSessionKeys.GivenName, user.GetPhotoAsBase64());

		return RedirectToPage("./Login", new { returnUrl = ReturnUrl });
	}
}
