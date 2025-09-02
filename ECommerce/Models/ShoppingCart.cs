namespace ECommerce.Models
{
    public class ShoppingCart
{
    public List<Product> Products { get; set; }

    public ShoppingCart()
    {
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        if (product != null)
        {
            Products.Add(product);
        }
    }
}
}