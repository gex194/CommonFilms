﻿namespace CommonFilms.DTOs;

public class RegisterInput
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}