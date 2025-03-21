using Microsoft.VisualBasic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class DemandesDTO
    {
        [JsonPropertyName("DEM_id")]
        public int DEM_id { get; set; }
        [JsonPropertyName("DEM_DteDemande")]
        public DateTime DEM_DteDemande { get; set; }
        [JsonPropertyName("DEM_DteDebut")]
        public DateTime DEM_DteDebut { get; set; }
        [JsonPropertyName("DEM_DteFin")]
        public DateTime DEM_DteFin { get; set; }
        [JsonPropertyName("DEM_Comm")]
        public string? DEM_Comm { get; set; }
        [JsonPropertyName("TYPE_Libelle")]
        public string? TYPE_Libelle { get; set; }
        [JsonPropertyName("TYPE_id")]
        public int TYPE_id{ get; set; }
        [JsonPropertyName("STAT_Libelle")]
        public string? STAT_Libelle { get; set; }
        [JsonPropertyName("DEM_Justificatif")]
        public string? DEM_Justificatif { get; set; }
        [JsonPropertyName("DEM_DureeHeures")]
        public Decimal DEM_DureeHeures { get; set; }
       
    }
}
