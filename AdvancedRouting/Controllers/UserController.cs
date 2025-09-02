using Microsoft.AspNetCore.Mvc;

namespace AdvancedRouting.Controllers
{
    public class UsersController : Controller
    {
        [Route("Users/{username}/Orders")]
        public IActionResult UserOrders(string username)
        {
            if (username == "admin")
            {
                return RedirectToAction("AdminDashboard");
            }
            else
            {
                ViewData["Username"] = username;
                return View();
            }
        }

        [Route("Admin/Dashboard")]
        public IActionResult AdminDashboard()
        {
            ViewData["Message"] = "Welcome, Admin!";
            return View();
        }
    }
}