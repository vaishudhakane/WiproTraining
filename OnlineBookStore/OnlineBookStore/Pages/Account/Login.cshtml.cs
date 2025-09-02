using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace OnlineBookStore.Pages.Account;

public class LoginModel(IUserRepository users) : PageModel
{
    [BindProperty] public string UserName { get; set; } = "";
    [BindProperty] public string Password { get; set; } = "";
    public string? ReturnUrl { get; set; }

    public void OnGet(string? returnUrl = null) => ReturnUrl = returnUrl;

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        var user = await users.FindByUserNameAsync(UserName);
        if (user is null || !await users.VerifyPasswordAsync(user, Password))
        {
            ModelState.AddModelError("", "Invalid credentials.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new("role", user.Role)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return LocalRedirect(returnUrl ?? "/");
    }
}
