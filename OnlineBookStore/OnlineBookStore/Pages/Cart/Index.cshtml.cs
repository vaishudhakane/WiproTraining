using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static OrdersController;

namespace OnlineBookStore.Pages.Cart;

public class IndexModel(IBookRepository repo) : PageModel
{
    public List<CartItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);

    public void OnGet()
    {
        Items = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CART") ?? new();
    }

    public async Task<IActionResult> OnPostAdd(int bookId, string title, decimal unitPrice, int quantity)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CART") ?? new();
        var existing = cart.FirstOrDefault(i => i.BookId == bookId);
        if (existing is not null)
        {
            cart = cart.Select(i => i.BookId == bookId ? i with { Quantity = i.Quantity + Math.Max(1, quantity) } : i).ToList();
        }
        else
        {
            cart.Add(new CartItem(bookId, title, Math.Max(1, quantity), unitPrice));
        }
        HttpContext.Session.SetObjectAsJson("CART", cart);
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int bookId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CART") ?? new();
        cart.RemoveAll(i => i.BookId == bookId);
        HttpContext.Session.SetObjectAsJson("CART", cart);
        return RedirectToPage();
    }
}
