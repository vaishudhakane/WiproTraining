using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class Cart
    {
        public List<Product> Products { get; set; }

        public Cart()
        {
            Products = new List<Product>();
        }
    }
}
