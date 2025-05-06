using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CommonFilms.Models.Entities;
using CommonFilms.Repositories.UserRepository;
using Microsoft.IdentityModel.Tokens;

namespace CommonFilms.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IServiceProvider _serviceProvider;

    public AuthService(IServiceProvider serviceProvider)
    {

        _serviceProvider = serviceProvider;
    }

    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var user = await userManager.GetByEmailAndPasswordAsync(email, password);
        return user;
    }

    public JwtSecurityToken GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        //key is for developement purposes only
        var key = new SymmetricSecurityKey("SecretKeySuperLongSuperStrongSuperHard"u8.ToArray());
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return token;
    }
    
    public async Task<User> RegisterAsync(User user)
    {
        throw new NotImplementedException();
    }
}