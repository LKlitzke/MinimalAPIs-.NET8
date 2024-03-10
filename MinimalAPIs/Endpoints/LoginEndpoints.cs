using Carter;
using Microsoft.AspNetCore.Authorization;
using MinimalAPIs.Models;
using MinimalAPIs.Services;

namespace MinimalAPIs.Endpoints
{
    public class LoginEndpoints : CarterModule
    {
        public LoginEndpoints() { }

        public override async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if(userModel == null)
                {
                    return Results.BadRequest("Login inválido");
                }
                if (userModel.UserName == "lucas.silva" && userModel.Password == "teste123#")
                {
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                        .Build();

                    var tokenString = tokenService.GetToken(
                        config.GetSection("Jwt")["Key"],
                        config.GetSection("Jwt")["Issuer"],
                        config.GetSection("Jwt")["Audience"],
                        userModel);

                    return Results.Ok(tokenString);
                }
                else
                    return Results.BadRequest();
            }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);

        }
    }
}
