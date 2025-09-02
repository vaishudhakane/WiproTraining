using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Models;

namespace OnlineBookStore.Data.Repositories;
public class UserRepository(ApplicationDbContext db) : IUserRepository
{
    private readonly PasswordHasher<ApplicationUser> _hasher = new();

    public Task<ApplicationUser?> FindByUserNameAsync(string userName)
        => db.Users.FirstOrDefaultAsync(u => u.UserName == userName);

    public async Task<ApplicationUser> CreateAsync(ApplicationUser user, string password)
    {
        user.PasswordHash = _hasher.HashPassword(user, password);
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user;
    }

    public Task<bool> VerifyPasswordAsync(ApplicationUser user, string password)
    {
        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash!, password);
        return Task.FromResult(result == PasswordVerificationResult.Success);
    }
}
