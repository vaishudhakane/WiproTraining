using System.Collections.Generic;
using ASP_Assignment2.Models;

namespace ASP_Assignment2.Services
{
    public class ItemService
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public ItemService()
        {
            Items.Add(new Item { Name = "Laptop" });
            Items.Add(new Item { Name = "Mobile" });
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }
    }
}
