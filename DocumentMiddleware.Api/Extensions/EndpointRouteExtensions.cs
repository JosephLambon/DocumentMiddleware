using DocumentMiddleware.Api.EndpointsHandlers;

namespace DocumentMiddleware.Api.Extensions
{
    public static class EndpointRouteExtensions
    {
        public static void RegisterAntiqueEndpoints(this IEndpointRouteBuilder app)
        {
            var imageEndpoints = app.MapGroup("/antiques")
                .WithOpenApi()
                .WithTags("Antique endpoints");

            imageEndpoints.MapPost("", AntiqueHandlers.CreateAntiqueAsync)
                .WithSummary("Add new antique")
                .DisableAntiforgery();

        }
    }
}
