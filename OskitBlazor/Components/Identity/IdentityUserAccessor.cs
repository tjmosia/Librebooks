using OskitBlazor.Extensions.Identity;
using OskitBlazor.Models.Entity.IdentitySpace;

namespace OskitBlazor.Components.Identity
{
    internal sealed class IdentityUserAccessor (UserManagerExt userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<User> GetRequiredUserAsync (HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
