using System.Text.Json.Serialization;

namespace DoConnect.Api.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        [JsonIgnore] 
        public Question Question { get; set; }

        public Answer Answer { get; set; }
    }
}
