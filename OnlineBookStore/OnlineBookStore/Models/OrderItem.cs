using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    [Range(1, 999)]
    public int Quantity { get; set; }
    [Range(0.01, 9999)]
    public decimal UnitPrice { get; set; }
}