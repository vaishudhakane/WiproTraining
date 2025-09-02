using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using OnlineBookStore.ViewModels;
using OnlineBookStore.Data;
public class BooksController(IBookRepository repo) : Controller
{
    [HttpGet("/books")]
    public async Task<IActionResult> Index()
    {
        var books = await repo.GetAllAsync();
        return View(new ViewModels.BookListVm { Books = books });
    }

    [HttpGet]
    [Route("books/{id:int}/{slug?}")]
    public async Task<IActionResult> Details(int id, string? slug)
    {
        var book = await repo.GetByIdAsync(id);
        if (book is null) return NotFound();

        var expectedSlug = book.Title.ToLower().Replace(' ', '-');
        if (!string.Equals(slug, expectedSlug, StringComparison.OrdinalIgnoreCase))
            return RedirectToActionPermanent(nameof(Details), new { id, slug = expectedSlug });

        return View(book);
    }
}
