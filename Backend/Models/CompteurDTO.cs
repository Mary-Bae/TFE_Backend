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
        [JsonPropertyName("DEM_DureeHeures")]
        public Decimal DEM_DureeHeures { get; set; }
        [JsonPropertyName("DureeRestante")]
        public Decimal DureeRestante { get; set; }
    }
}
