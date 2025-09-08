namespace DoConnect.Api.Dtos
{
    public class CreateQuestionDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public IFormFile[] Images { get; set; }
    }
}
