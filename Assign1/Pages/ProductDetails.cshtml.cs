using Assign1.Models;
using Assign1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assign1.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ProductService _productService;

        public ProductDetailsModel(ProductService productService)
        {
            _productService = productService;
        }

        public Product ?Product { get; set; }

        public IActionResult OnGet(int id)
        {
            Product = _productService.GetProduct(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
