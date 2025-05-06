using System.IdentityModel.Tokens.Jwt;
using CommonFilms.Models.Entities;

namespace CommonFilms.Services.AuthService;

public interface IAuthService
{
    public Task<User?> ValidateUserAsync(string email, string password);
    public Task<User> RegisterAsync(User user);
    public JwtSecurityToken GenerateJwtToken(User user);
}