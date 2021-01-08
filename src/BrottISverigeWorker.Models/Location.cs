using System.Text.Json.Serialization;

namespace BrottISverigeWorker.Models
{
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("gps")]
        public string Gps { get; set; }
    }
}
