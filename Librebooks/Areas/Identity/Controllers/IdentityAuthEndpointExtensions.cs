using Librebooks.Areas.Identity.Services;

using Microsoft.AspNetCore.Mvc;

using static Librebooks.Areas.Identity.Models.AuthReqModels;

namespace Librebooks.Areas.Identity.Controllers
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder Map (this IEndpointRouteBuilder endpoints)
        {
            var authGroup = endpoints.MapGroup("/auth");

            authGroup.MapGet("", ([FromBody] UsernameModel input, UserManagerExtension userManager) =>
            {
                Results.Ok();
            });

            return authGroup;
        }
    }
}
