using ASP_Assignment2.Models;
using ASP_Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Assignment2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ItemService _itemService;

        public IndexModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public List<Item> Items => _itemService.Items;

        public void OnGet()
        {
        }
    }
}
