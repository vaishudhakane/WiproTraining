namespace DoConnect.Api.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public string Status { get; set; } = "Pending";
        public Question Question { get; set; }
        public User User { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
