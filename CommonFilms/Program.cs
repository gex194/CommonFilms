using System.Text;
using CommonFilms.Data;
using CommonFilms.Repositories.MovieRepository;
using CommonFilms.Repositories.UserRepository;
using CommonFilms.Services.AuthService;
using CommonFilms.Services.MovieService;
using CommonFilms.Services.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
});

//Adding HttpClient to TMDB
builder.Services.AddHttpClient("TheMovieDB", client =>
{
    var apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

//Adding JWT Authentication
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey("SecretKeySuperLongSuperStrongSuperHard"u8.ToArray())
    };
});

//Adding Db Context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CommonFilms"));

//Registering Repos
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Registering Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddSingleton<IAuthService, AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    ApplicationDbContext.SeedData(dbContext);
}

app.UseCors();

app.Run();