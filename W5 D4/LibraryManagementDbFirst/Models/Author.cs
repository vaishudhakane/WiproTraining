using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementDbFirst.Models;

public partial class Author
{
    [Key]
    public int AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Bio { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
