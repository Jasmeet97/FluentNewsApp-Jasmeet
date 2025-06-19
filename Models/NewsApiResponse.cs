using System.Text.Json.Serialization;

namespace FluentNewsApp_Jasmeet.Models
{
    public class NewsApiResponse
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("articles")]
        public List<Article>? Articles { get; set; }
    }
}
