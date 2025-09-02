using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Extensions;

namespace ECommerce.Controllers
{
    public class ShoppingCartController : Controller
{
    private List<Product> products = new List<Product>()
    {
        new Product { Id = 1, Category = "Electronics", Name = "Laptop", Price = 1000 },
        new Product { Id = 2, Category = "Electronics", Name = "Phone", Price = 500 },
        new Product { Id = 3, Category = "Fashion", Name = "Shirt", Price = 20 },
    };

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObject<ShoppingCart>("Cart");
        if (cart == null)
    {
        cart = new ShoppingCart();
    }
        return View(cart);
    }

     public IActionResult AddToCart(int id)
    {
        var product = Product.GetProduct(id, products);
    if (product != null)
    {
        var cart = HttpContext.Session.GetObject<ShoppingCart>("Cart");
        if (cart == null)
        {
            cart = new ShoppingCart();
        }
        cart.AddProduct(product);
        HttpContext.Session.SetObject("Cart", cart);
    }
    return RedirectToAction("Index");
    }
}


}



