using CommonFilms.Data;
using CommonFilms.Repositories.MovieRepository;
using CommonFilms.Repositories.UserRepository;
using CommonFilms.Services.MovieService;
using CommonFilms.Services.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Adding Db Context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CommonFilms"));

//Registering Repos
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Registering Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();