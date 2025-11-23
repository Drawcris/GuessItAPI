using GuessIt.Models;
using GuessIt.Data;
using GuessIt.DTOs;
using GuessIt.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GuessIt.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;

    public AuthRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    public async Task<bool> CreateUserAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }
    
    public async Task<IQueryable<User?>> GetAllUsers()
    {
        var users = _userManager.Users.AsQueryable();
        return await Task.FromResult(users);
    }
}