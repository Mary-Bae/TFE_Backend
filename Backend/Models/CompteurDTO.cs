using Microsoft.VisualBasic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class CompteurDTO
    {
        [JsonPropertyName("TAEM_id")]
        public int TAEM_id { get; set; }
        [JsonPropertyName("TYPE_Libelle")]
        public string? TYPE_Libelle { get; set; }
        [JsonPropertyName("TAEM_NbrJoursAn")]
        public Decimal TAEM_NbrJoursAn { get; set; }
        [JsonPropertyName("TAEM_Heures")]
        public Decimal TAEM_Heures { get; set; }
        [JsonPropertyName("TAEM_HeuresPrises")]
        public Decimal TAEM_HeuresPrises { get; set; }
        [JsonPropertyName("TAEM_HeuresRestantes")]
        public Decimal TAEM_HeuresRestantes { get; set; }
    }
}
