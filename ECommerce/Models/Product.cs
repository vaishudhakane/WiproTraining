namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    public static Product GetProduct(int id, List<Product> products)
    {
        return products.FirstOrDefault(p => p.Id == id);
    }
    }

}
