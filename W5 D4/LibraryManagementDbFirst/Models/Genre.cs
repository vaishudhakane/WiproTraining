using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDbFirst.Models;

public partial class Genre
{
    [Key]
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    [ForeignKey("GenresGenreId")]
    [InverseProperty("GenresGenres")]
    public virtual ICollection<Book> BooksBooks { get; set; } = new List<Book>();
}
