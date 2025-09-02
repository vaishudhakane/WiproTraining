using System.ComponentModel.DataAnnotations;
using OnlineBookStore.Models.Validation;

namespace OnlineBookStore.Models;

public class Book
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = "";

    [Required, StringLength(150)]
    public string Author { get; set; } = "";

    [Required, Isbn]
    public string ISBN { get; set; } = "";

    [PriceRange(1, 999)]
    public decimal Price { get; set; }
}
