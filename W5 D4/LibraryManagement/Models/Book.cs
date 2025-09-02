namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // Many-to-many with Genre
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
