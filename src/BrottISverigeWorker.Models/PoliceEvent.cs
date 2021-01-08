using System;
using System.Text.Json.Serialization;

namespace BrottISverigeWorker.Models
{
    public class PoliceEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("datetime")]
        public DateTime DateTime { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("summary")]
        public string Summary { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }
}
