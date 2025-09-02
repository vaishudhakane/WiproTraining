using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Prep1.Data;
using System.Threading.Tasks;

namespace Prep1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser > _userManager;

        public AdminController(UserManager<ApplicationUser > userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var users = _userManager.Users;
            ViewBag.Message = "Welcome, Admin! You have access to the Admin Dashboard.";
            return View(users);
        }
    }
}
