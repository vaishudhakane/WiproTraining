using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Models;

namespace OnlineBookStore.Pages.Books;

public class CreateModel(IBookRepository repo) : PageModel
{
    [BindProperty] public Book Book { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        if (await repo.IsIsbnTakenAsync(Book.ISBN))
        {
            ModelState.AddModelError("Book.ISBN", "ISBN already exists.");
            return Page();
        }

        await repo.AddAsync(Book);
        return Redirect("/books");
    }
}
