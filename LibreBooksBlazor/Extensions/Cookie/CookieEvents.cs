using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using OskitBlazor.Components.Identity.Pages;

namespace OskitBlazor.Extensions.Cookie
{
    public class CookieEvents : CookieAuthenticationEvents
    {
        public override Task RedirectToLogin (RedirectContext<CookieAuthenticationOptions> context)
        {
            context.RedirectUri = IdentityURIs.Login;

            return base.RedirectToLogin(context);
        }
    }
}
