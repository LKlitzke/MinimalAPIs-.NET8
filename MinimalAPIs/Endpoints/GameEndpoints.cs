using Carter;
using Carter.OpenApi;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalAPIs.Context;
using MinimalAPIs.Models;

namespace MinimalAPIs.Endpoints
{
    public class GameEndpoints : CarterModule
    {
        private readonly IMapper _mapper;

        public GameEndpoints(IMapper mapper)
        {
            _mapper = mapper;
        }
        public override async void AddRoutes(IEndpointRouteBuilder app)
        {
            // GET
            app.MapGet("/games", async (AppDbContext db) => await db.Games.ToListAsync())
              .IncludeInOpenApi()
               .WithTags("Games");

            // GET BY ID
            app.MapGet("/games/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Games.FindAsync(id)
                    is Game game
                        ? Results.Ok(game)
                        : Results.NotFound();

            }).IncludeInOpenApi()
                .WithTags("Games");

            // POST
            app.MapPost("/game", async (Game game, AppDbContext db) =>
            {
                db.Games.Add(game);
                await db.SaveChangesAsync();

                return Results.Created($"/games/{game.GameId}", game);
            }).IncludeInOpenApi()
            .WithTags("Games");

            // PUT
            app.MapPut("/games/{id:int}", async (int id, Game gameInput, AppDbContext db) =>
            {
                if(gameInput.GameId != id)
                {
                    return Results.BadRequest();
                }

                var gameDb = await db.Games.FindAsync(id);

                if(gameDb != null)
                {
                    gameDb = _mapper.Map(gameInput, gameDb);

                    await db.SaveChangesAsync();
                    return Results.Ok(gameDb);
                }
                else
                    return Results.NotFound();

            }).IncludeInOpenApi()
            .WithTags("Games");

            // DELETE
            app.MapDelete("/games/{id:int}", async (int id, AppDbContext db) =>
            {
                var gameDb = await db.Games.FindAsync(id);

                if (gameDb != null)
                {
                    db.Games.Remove(gameDb);
                    await db.SaveChangesAsync();
                    return Results.Ok(gameDb);
                }
                else
                    return Results.NotFound();

            }).IncludeInOpenApi()
                .WithTags("Games");
        }
    }
}
