using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Models;

namespace OnlineBookStore.Data;

public static class SeedData
{
    public static async Task EnsureSeedAsync(ApplicationDbContext db)
    {
        if (!await db.Books.AnyAsync())
        {
            db.Books.AddRange(
                new Book { Title = "Clean Code", Author = "Robert C. Martin", ISBN = "9780132350884", Price = 39.99m },
                new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", ISBN = "9780201616224", Price = 44.95m },
                new Book { Title = "CLR via C#", Author = "Jeffrey Richter", ISBN = "9780735667457", Price = 49.99m }
            );
        }

        if (!await db.Users.AnyAsync())
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Role = "Admin"
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

            var user = new ApplicationUser
            {
                UserName = "user",
                Role = "User"
            };
            user.PasswordHash = hasher.HashPassword(user, "User@123");

            db.Users.AddRange(admin, user);
        }

        await db.SaveChangesAsync();
    }
}
