using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrebooksRazor.Areas.Identity.Pages.Auth;

public class LoginModel : PageModel
{
	[FromQuery(Name = "returnUrl")]
	public string? ReturnUrl { get; set; }
	public void OnGet ()
	{
		if (HttpContext.Session.GetString(AuthSessionKeys.Email) is null)
			Response.Redirect(Url.Page("./UsernameEntry", new { returnUrl = ReturnUrl })!);
	}
}
