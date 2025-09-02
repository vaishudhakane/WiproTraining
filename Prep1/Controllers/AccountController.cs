using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Prep1.Data;
using Prep1.Models;
using System.Threading.Tasks;

namespace Prep1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser > _userManager;
        private readonly SignInManager<ApplicationUser > _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser > userManager,
            SignInManager<ApplicationUser > signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (roles.Contains("User "))
                    {
                        return RedirectToAction("User Profile", "Account");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "User ")]
        public IActionResult UserProfile()
        {
            ViewBag.Message = "Welcome, User! Here is your profile information.";
            return View();
        }

        // Seed roles and users (for demo purposes)
        [HttpGet]
        public async Task<IActionResult> Seed()
        {
            // Create roles if they don't exist
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await _roleManager.RoleExistsAsync("User "))
                await _roleManager.CreateAsync(new IdentityRole("User "));

            // Create admin user
            var adminUser  = await _userManager.FindByNameAsync("admin");
            if (adminUser  == null)
            {
                adminUser  = new ApplicationUser  { UserName = "admin", Email = "admin@example.com" };
                await _userManager.CreateAsync(adminUser , "Admin@123");
                await _userManager.AddToRoleAsync(adminUser , "Admin");
            }

            // Create normal user
            var normalUser  = await _userManager.FindByNameAsync("user1");
            if (normalUser  == null)
            {
                normalUser  = new ApplicationUser  { UserName = "user1", Email = "user1@example.com" };
                await _userManager.CreateAsync(normalUser , "User @123");
                await _userManager.AddToRoleAsync(normalUser , "User ");
            }

            return Content("Seeded users and roles.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
