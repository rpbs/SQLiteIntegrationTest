using System.Text.Json.Serialization;

namespace SQLiteApplication.DTO
{
    public class ProductDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("description")] 
        public string Description { get; set; } = null!;

        [JsonPropertyName("value")] 
        public double Value { get; set; }
    }
}
