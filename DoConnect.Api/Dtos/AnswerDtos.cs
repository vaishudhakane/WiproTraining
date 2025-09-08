namespace DoConnect.Api.Dtos
{
    public class CreateAnswerDto
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public IFormFile[] Images { get; set; }
    }
}
