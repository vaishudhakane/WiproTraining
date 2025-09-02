using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using System.Security.Claims;

[Authorize]
public class OrdersController(IOrderRepository orders, IBookRepository books) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Summary()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CART") ?? new();
        if (cart.Count == 0) return View("Empty");

        var vm = new ViewModels.OrderSummaryVm
        {
            Items = cart,
            Total = cart.Sum(c => c.Quantity * c.UnitPrice)
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CART") ?? new();
        if (cart.Count == 0) return RedirectToAction(nameof(Summary));

        var userName = User.Identity!.Name!;
        var order = new Order
        {
            UserName = userName,
            Items = cart.Select(c => new OrderItem
            {
                BookId = c.BookId,
                Quantity = c.Quantity,
                UnitPrice = c.UnitPrice
            }).ToList()
        };

        var saved = await orders.CreateAsync(order);
        HttpContext.Session.Remove("CART");
        return View("Confirmation", saved);
    }
}

// cart DTO + session helpers
public record CartItem(int BookId, string Title, int Quantity, decimal UnitPrice);

public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
        => session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));

    public static T? GetObjectFromJson<T>(this ISession session, string key)
        => session.TryGetValue(key, out _) ?
           System.Text.Json.JsonSerializer.Deserialize<T>(session.GetString(key)!) : default;
}
