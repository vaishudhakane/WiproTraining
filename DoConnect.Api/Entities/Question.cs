namespace DoConnect.Api.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public User User { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
