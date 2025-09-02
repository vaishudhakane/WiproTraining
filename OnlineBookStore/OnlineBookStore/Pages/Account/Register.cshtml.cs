using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Models;

namespace OnlineBookStore.Pages.Account;

public class RegisterModel(IUserRepository users) : PageModel
{
    [BindProperty] public string UserName { get; set; } = "";
    [BindProperty] public string Password { get; set; } = "";
    [BindProperty] public string Role { get; set; } = "User";

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
        {
            ModelState.AddModelError("", "Username and password are required.");
            return Page();
        }

        var existing = await users.FindByUserNameAsync(UserName);
        if (existing is not null)
        {
            ModelState.AddModelError("", "Username already taken.");
            return Page();
        }

        await users.CreateAsync(new ApplicationUser { UserName = UserName, Role = Role }, Password);
        return RedirectToPage("/Account/Login");
    }
}
