using Microsoft.AspNetCore.Mvc; 
using ECommerce.Models;
namespace ECommerce.Controllers
{
    public class ProductsController : Controller
{
    private List<Product> products = new List<Product>()
    {
        new Product { Id = 1, Category = "Electronics", Name = "Laptop", Price = 1000 },
        new Product { Id = 2, Category = "Electronics", Name = "Phone", Price = 500 },
        new Product { Id = 3, Category = "Fashion", Name = "Shirt", Price = 20 },
    };

    public IActionResult Index()
    {
        return View(products);
    }

    
[Route("Products/{category}/{id}")]
public IActionResult Details(string category, int id)
{
    var product = products.FirstOrDefault(p => p.Category == category && p.Id == id);
    return View(product);
}

    [Route("Products/Filter/{category}/{priceRange}")]
    public IActionResult Filter(string category, string priceRange)
    {
        var filteredProducts = products.Where(p => p.Category == category);
        if (priceRange == "low")
        {
            filteredProducts = filteredProducts.Where(p => p.Price < 100);
        }
        else if (priceRange == "high")
        {
            filteredProducts = filteredProducts.Where(p => p.Price >= 100);
        }
        return View(filteredProducts.ToList());
    }
}
}