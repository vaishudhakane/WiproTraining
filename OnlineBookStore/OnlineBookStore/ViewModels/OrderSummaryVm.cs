using OnlineBookStore.Models;
using static OrdersController;

namespace OnlineBookStore.ViewModels;

public class OrderSummaryVm
{
    public List<CartItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}
