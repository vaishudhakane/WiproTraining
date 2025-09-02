using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Order order)
        {
            // Place order logic
            return RedirectToAction("Billing");
        }

        public IActionResult Billing()
        {
            return View();
        }
    }


}
