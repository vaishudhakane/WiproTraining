using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace OnlineBookStore.Models;

public class ApplicationUser : IdentityUser
{
    public int Id { get; set; }

    [Required, StringLength(64)]
    public string UserName { get; set; } = "";

    public string? PasswordHash { get; set; }

    [Required, StringLength(16)]
    public string Role { get; set; } = "User"; // "Admin" or "User"
}
