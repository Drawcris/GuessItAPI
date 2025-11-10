using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GuessIt.DTOs;
using GuessIt.Interfaces;
using GuessIt.Repositories;
using GuessIt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GuessIt.Services;

public class AuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(IAuthRepository authRepository, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    
    public async Task<(bool Success, string Message)> RegisterAsync(RegisterDTO dto)
    {
        var existing = await _authRepository.GetUserByEmailAsync(dto.Email);
        if (existing != null)
        {
            return (false, "User with this email already exists.");
        }

        var user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email,
            RoleType = dto.RoleType,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await _authRepository.CreateUserAsync(user, dto.Password);
        if (!result)
        {
            return (false, "Failed to create user.");
        }

        return (true, "User registered successfully.");
    }
    public async Task<(bool Success, string Message)> LoginAsync(LoginDTO dto)
    {
        var user = await _authRepository.GetUserByEmailAsync(dto.Email);
        if (user == null)
        {
            return (false, "User not found.");
        }

        var passwordValid = await _authRepository.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
        {
            return (false, "Invalid password.");
        }

        return (true, "Login successful.");
    }
    public async Task<string> GenerateJwtTokenAsync(string email)
    {
        var user = await _authRepository.GetUserByEmailAsync(email);
        

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("Role", user.RoleType.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public async Task<(bool Success, string Message)> ChangePasswordAsync(ChangePasswordDTO dto, string email)
    {
        var user = await _authRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            return (false, "User not found.");
        }

        var oldPasswordValid = await _authRepository.CheckPasswordAsync(user, dto.CurrentPassword);
        if (!oldPasswordValid)
        {
            return (false, "Current password is incorrect.");
        }

        var result = await _authRepository.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        if (result.Succeeded)
        {
            return (true, "Password changed successfully.");
        }

        return (false, "Failed to change password.");
    }
    public async Task<Dictionary<string, string>?> MyProfileAsync(string email)
    {
        var user = await _authRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            return null;
        }

        var profile = new Dictionary<string, string>
        {
            { "FirstName", user.FirstName },
            { "LastName", user.LastName },
            { "Email", user.Email },
            { "RoleType", user.RoleType.ToString() },
            { "CreatedAt", user.CreatedAt.ToString("u") },
            { "UpdatedAt", user.UpdatedAt.ToString("u") }
        };
        return profile;
    }

}