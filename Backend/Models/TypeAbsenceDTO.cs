using Microsoft.VisualBasic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class TypeAbsenceDTO
    {
        [JsonPropertyName("TAEM_id")]
        public int TAEM_id { get; set; }
        [JsonPropertyName("TYPE_Libelle")]
        public string? TYPE_Libelle { get; set; }
        [JsonPropertyName("TAEM_NbrJoursAn")]
        public decimal TAEM_NbrJoursAn { get; set; }
        [JsonPropertyName("TAEM_NbrJoursSemaine")]
        public decimal TAEM_NbrJoursSemaine { get; set; }
        [JsonPropertyName("TAEM_Heures")]
        public decimal TAEM_Heures { get; set; }
       
    }
}
