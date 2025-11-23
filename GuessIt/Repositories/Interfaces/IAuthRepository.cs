using GuessIt.Models;
using Microsoft.AspNetCore.Identity;

namespace GuessIt.Repositories.Interfaces;

public interface IAuthRepository
{
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<User?> GetUserByIdAsync(string id);
    public Task<bool> CreateUserAsync(User user, string password);
    public Task<bool> CheckPasswordAsync(User user, string password);
    public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    public Task<IQueryable<User?>> GetAllUsers();
}