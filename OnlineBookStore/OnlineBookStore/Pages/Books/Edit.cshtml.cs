using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Models;

namespace OnlineBookStore.Pages.Books;

public class EditModel(IBookRepository repo) : PageModel
{
    [BindProperty] public Book Book { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var b = await repo.GetByIdAsync(id);
        if (b is null) return NotFound();
        Book = b;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        if (await repo.IsIsbnTakenAsync(Book.ISBN, Book.Id))
        {
            ModelState.AddModelError("Book.ISBN", "ISBN already exists.");
            return Page();
        }

        await repo.UpdateAsync(Book);
        return Redirect("/books");
    }
}
