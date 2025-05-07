using System.IdentityModel.Tokens.Jwt;
using CommonFilms.DTOs;
using CommonFilms.Models.Entities;
using CommonFilms.Services.AuthService;
using CommonFilms.Services.UserService;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterInput registerInput)
    {
        var (valid, message) = _authService.ValidateRegisterInput(registerInput);
        if (!valid)
        {
            return BadRequest(message);
        }
        var newUser = new User()
        {
            Name = registerInput.Name,
            Email = registerInput.Email,
            Password = registerInput.Password,
            IsActive = true,
            IsAdmin = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        await _authService.RegisterAsync(newUser);
        return Ok(newUser);
    }
}