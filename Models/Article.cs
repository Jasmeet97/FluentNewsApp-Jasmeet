using System.Text.Json.Serialization;

namespace FluentNewsApp_Jasmeet.Models
{
    public class Article
    {
        [JsonPropertyName("title")]
        public required string Headline { get; set; }
        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }
    }
}
