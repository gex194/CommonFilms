using System.IdentityModel.Tokens.Jwt;
using CommonFilms.DTOs;
using CommonFilms.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CommonFilms.Controllers;

[Route("api/{controller}")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginInput loginInput)
    {
        var user = await _authService.ValidateUserAsync(loginInput.Email, loginInput.Password);
        if (user == null)
        {
            return BadRequest("Invalid credentials");
        }

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}