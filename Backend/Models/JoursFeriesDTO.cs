using Microsoft.VisualBasic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class JoursFeriesDTO
    {
        [JsonPropertyName("JFER_id")]
        public int JFER_id { get; set; }
        [JsonPropertyName("JFER_DteFerie")]
        public DateTime JFER_DteFerie { get; set; }
        [JsonPropertyName("JFER_Description")]
        public string? JFER_Description { get; set; }
    }
}
