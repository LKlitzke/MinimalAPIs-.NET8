using Carter;
using Carter.OpenApi;

namespace MinimalAPIs.Models
{
    public class HomeModule : CarterModule
    {
        public HomeModule()
        {
            WithTags("Module");
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", () =>
            {
                return "Hello Carter!";
            })
            .IncludeInOpenApi()
            .RequireAuthorization();
        }
    }
}
