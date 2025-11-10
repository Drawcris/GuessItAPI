namespace GuessIt.Repositories;
using GuessIt.Models;
using Microsoft.AspNetCore.Identity;

public interface IAuthRepository
{
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<bool> CreateUserAsync(User user, string password);
    public Task<bool> CheckPasswordAsync(User user, string password);
    public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
}