using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBookStore.Filters;

// Example of a custom authorization filter (used per-action if you want extra checks)
public class RoleAuthorizeFilter(string role) : IAsyncAuthorizationFilter
{
    private readonly string _role = role;

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnUrl = context.HttpContext.Request.Path });
            return Task.CompletedTask;
        }

        var hasRole = user.Claims.Any(c => c.Type == "role" && c.Value == _role);
        if (!hasRole)
        {
            context.Result = new ForbidResult();
        }

        return Task.CompletedTask;
    }
}
