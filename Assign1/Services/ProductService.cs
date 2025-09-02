using System.Xml.Serialization;
using Assign1.Models;

namespace Assign1.Services
{
    public class ProductService
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public ProductService()
        {
            // Initialize products
            Products.Add(new Product
            {
                ProductID = 1,
                Name = "Product 1",
                Description = "Description 1",
                Categories = new List<Category>
                {
                    new Category { CategoryID = 1, CategoryName = "Category 1" },
                    new Category { CategoryID = 2, CategoryName = "Category 2" }
                }
            });
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public Product GetProduct(int id)
        {
            return Products.Find(p => p.ProductID == id);
        }
    }
}
