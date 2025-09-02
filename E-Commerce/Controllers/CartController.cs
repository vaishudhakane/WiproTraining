using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;



namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly Cart _cart;

        public CartController()
        {
            _cart = new Cart();
        }

        public IActionResult Index()
        {
           
            
            return View(_cart);
        }


        public IActionResult AddProduct(Product product)
        {

            _cart.Products.Add(product);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveProduct(Product product)
        {
            _cart.Products.Remove(product);
            return RedirectToAction("Index");
        }
    }



}
