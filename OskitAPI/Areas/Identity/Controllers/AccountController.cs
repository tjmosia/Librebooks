using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OskitAPI.Extensions.Identity;
using OskitAPI.Extensions.Mvc;

namespace OskitAPI.Areas.Identity.Controllers
{
    [ApiController]
    [Authorize]
    [Route("account")]
    public class AccountController (UserManagerExt userManager, ILogger<SessionControllerBase> logger)
        : SessionControllerBase(userManager: userManager, logger: logger)
    {

        [HttpGet]
        [Route("claims")]
        public async Task<IActionResult> GetClaimsAsync ()
        {
            logger!.LogInformation("{token}", HttpContext.Request.Headers.Authorization.FirstOrDefault());
            var username = User.Identity!.Name;
            if (username == null)
                return Forbid();
            var user = await userManager!.FindByNameAsync(username);
            if (user == null)
                return Forbid();
            return Ok(await userManager.GetClaimsAsync(user));
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IActionResult> GetRolesAsync ()
        {
            var user = await userManager!.FindByNameAsync(User.Identity!.Name!);
            if (user == null)
                return Forbid();
            return Ok(await userManager.GetRolesAsync(user));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUserAsync ()
        {
            var user = await userManager!.FindByNameAsync(User.Identity!.Name!);

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                Birthday = user.BirthDay,
                Gender = user.Gender,
                DateJoined = user.DateRegistered
            });
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout ()
        {
            HttpContext.Response.Cookies.Delete(JwtTokenKeys.AccessToken);
            return Ok();
        }


    }
}
