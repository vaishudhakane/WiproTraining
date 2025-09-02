using Microsoft.AspNetCore.Mvc;
using AdvancedRouting.Models;
namespace AdvancedRouting.Controllers
{
    public class ProductsController : Controller
    {
        [Route("Products/{category}/{id}")]
        public IActionResult ProductDetails(string category, int id)
        {
            var product = new Product
    {
        Category = category ?? "Unknown",
        Id = id
    };
    return View(product);
        }
    }
}