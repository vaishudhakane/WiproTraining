   using Microsoft.AspNetCore.Mvc;
   using Microsoft.AspNetCore.Mvc.RazorPages;
   using ProductManagementApp.Models;

   namespace ProductManagementApp.Pages.Products
   {
       public class CreateModel : PageModel
       {
           [BindProperty]
           public Product Product { get; set; } = new Product();
           private static List<Product> Products = new List<Product>();

           public void OnGet()
           {
           }

           public IActionResult OnPost()
           {
               if (!ModelState.IsValid)
               {
                   return Page();
               }

               // Here you would typically save the product to a database
               // For now, we will just redirect to a list page or display a success message
                Products.Add(Product);
               return RedirectToPage("Index");
           }
       }
   }
   