namespace MacbooksAPI.Areas.Identity.Controllers
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder Map (this IEndpointRouteBuilder endpoints)
        {
            var authGroup = endpoints.MapGroup("/auth");








            return authGroup;
        }
    }
}
