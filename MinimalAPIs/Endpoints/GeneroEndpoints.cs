using AutoMapper;
using Carter;
using Carter.OpenApi;
using Microsoft.EntityFrameworkCore;
using MinimalAPIs.Context;
using MinimalAPIs.Models;

namespace MinimalAPIs.Endpoints
{
    public class GeneroEndpoints : CarterModule
    {
        private readonly IMapper _mapper;

        public GeneroEndpoints(IMapper mapper)
        {
            _mapper = mapper;
        }
        public override async void AddRoutes(IEndpointRouteBuilder app)
        {
            //app.MapGet("/TESTE", () =>
            //{
            //    return "Hello Carter!";
            //})
            //.IncludeInOpenApi()
            //.RequireAuthorization();

            // GET
            app.MapGet("/generos", async (AppDbContext db) => await db.Generos.ToListAsync())
              .IncludeInOpenApi()
               .WithTags("Gêneros");

            // GET BY ID
            app.MapGet("/generos/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Generos.FindAsync(id)
                    is Genero genero
                        ? Results.Ok(genero)
                        : Results.NotFound();

            }).IncludeInOpenApi()
                .WithTags("Gêneros");
            
            // POST
            app.MapPost("/genero", async (Genero genero, AppDbContext db) =>
            {
                db.Generos.Add(genero);
                await db.SaveChangesAsync();

                return Results.Created($"/generos/{genero.GeneroId}", genero);
            }).IncludeInOpenApi()
            .WithTags("Gêneros");

            // PUT
            app.MapPut("/generos/{id:int}", async (int id, Genero generoInput, AppDbContext db) =>
            {
                if(generoInput.GeneroId != id)
                {
                    return Results.BadRequest();
                }

                var generoDb = await db.Generos.FindAsync(id);

                if(generoDb != null)
                {
                    generoDb = _mapper.Map(generoInput, generoDb);

                    await db.SaveChangesAsync();
                    return Results.Ok(generoDb);
                }
                else
                    return Results.NotFound();

            }).IncludeInOpenApi()
            .WithTags("Gêneros");

            // DELETE
            app.MapDelete("/generos/{id:int}", async (int id, AppDbContext db) =>
            {
                var generoDb = await db.Generos.FindAsync(id);

                if (generoDb != null)
                {
                    db.Generos.Remove(generoDb);
                    await db.SaveChangesAsync();
                    return Results.Ok(generoDb);
                }
                else
                    return Results.NotFound();

            }).IncludeInOpenApi()
                .WithTags("Gêneros");
        }
    }
}
