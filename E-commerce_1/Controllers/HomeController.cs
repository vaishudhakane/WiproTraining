using System.Diagnostics;
using E_commerce_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_1.Controllers
{
    public class HomeController : Controller
    {
        // Simulated user store
        private static List<User> users = new List<User>();
        private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", ImageUrl = "url1" },
        new Product { Id = 2, Name = "Product 2", ImageUrl = "url2" }
    };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            // Logic to authenticate user
            // Simulating session for username
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Home");
        }

        public IActionResult Home()
        {
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            return View(products);
        }

        public IActionResult AddToOrder(int productId)
        {
            // Logic to add a product to the order
            return RedirectToAction("Order");
        }

        public IActionResult Order()
        {
            // Logic for displaying order details
            return View();
        }
    }

}
