using Microsoft.AspNetCore.Mvc;
using Librebooks.Areas.Identity.Services;

namespace Librebooks.Extensions.Mvc
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class SessionControllerBase
        : ControllerBase
    {
        protected readonly UserManagerExtension? userManager;
        protected readonly SignInManagerExtension? signInManager;
        protected readonly ILogger<SessionControllerBase>? logger;

        public SessionControllerBase (
            UserManagerExtension? userManager = null,
            SignInManagerExtension? signInManager = null,
            ILogger<SessionControllerBase>? logger = null)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }
    }
}
