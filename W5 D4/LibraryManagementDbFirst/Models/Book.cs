using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDbFirst.Models;

[Index("AuthorId", Name = "IX_Books_AuthorId")]
public partial class Book
{
    [Key]
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    public virtual Author Author { get; set; } = null!;

    [ForeignKey("BooksBookId")]
    [InverseProperty("BooksBooks")]
    public virtual ICollection<Genre> GenresGenres { get; set; } = new List<Genre>();
}
