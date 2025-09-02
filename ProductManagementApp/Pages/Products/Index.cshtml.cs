   using Microsoft.AspNetCore.Mvc.RazorPages;
   using ProductManagementApp.Models;
   using System.Collections.Generic;

   namespace ProductManagementApp.Pages.Products
   {
       public class IndexModel : PageModel
       {
           public List<Product> Products { get; set; } = new List<Product>();

           public void OnGet()
           {
               
               Products.Add(new Product
               {
                   ProductID = 1,
                   Name = "Laptop",
                   Description = "This is an laptop",
                   Categories = new List<Category>
                   {
                       new Category { CategoryID = 1, CategoryName = "Dell" },
                       new Category { CategoryID = 2, CategoryName = "HP" }
                   }
               });
               Products.Add(new Product
               {
                   ProductID = 2,
                   Name = "Desktop",
                   Description = "This is a desktop computer",
                   Categories = new List<Category>
                   {
                       new Category { CategoryID = 3, CategoryName = "Sony" }
                   }
               });
           }
       }
   }
   