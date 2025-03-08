using LibreBooks.Extensions.Identity;

using Microsoft.AspNetCore.Mvc;

using static LibreBooks.Areas.Identity.Models.AuthReqModels;

namespace LibreBooks.Areas.Identity.Controllers
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder Map (this IEndpointRouteBuilder endpoints)
        {
            var authGroup = endpoints.MapGroup("/auth");

            authGroup.MapGet("", ([FromBody] UsernameModel input, UserManagerExt userManager) =>
            {
                Results.Ok();
            });

            return authGroup;
        }
    }
}
