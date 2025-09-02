using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineBookStore.Pages.Books;

public class DeleteModel(IBookRepository repo) : PageModel
{
    public async Task<IActionResult> OnPostAsync(int id)
    {
        await repo.DeleteAsync(id);
        return Redirect("/books");
    }
}
