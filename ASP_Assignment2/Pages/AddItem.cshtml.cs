using ASP_Assignment2.Models;
using ASP_Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ASP_Assignment2.Pages
{
    public class AddItemModel : PageModel
    {
        private readonly ItemService _itemService;

        [BindProperty]
        public Item Item { get; set; }

        public AddItemModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _itemService.AddItem(Item);
            return RedirectToPage("Index");
        }
    }
}
