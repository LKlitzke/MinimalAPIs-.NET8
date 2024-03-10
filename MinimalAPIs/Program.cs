using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalAPIs.AppServicesExtensions;
using MinimalAPIs.Context;
using MinimalAPIs.Models;
using MinimalAPIs.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Extension Methods
builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

builder.Services.AddCarter();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//builder.Services.AddSingleton<ITokenService>(new TokenService());
//builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
var env = app.Environment;
app.UseExceptionHandling(env)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapCarter());

app.UseHttpsRedirection();

app.Run();