using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models;

public class Order
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
}