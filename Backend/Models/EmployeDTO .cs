using Microsoft.VisualBasic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class EmployeDTO
    {
        [JsonPropertyName("EMP_id")]
        public int EMP_id {  get; set; }
        [JsonPropertyName("EMP_Nom")]
        public string? EMP_Nom { get; set; }
        [JsonPropertyName("EMP_Prenom")]
        public string? EMP_Prenom { get; set; }
        [JsonPropertyName("EMP_Pren2")]
        public string? EMP_Pren2 { get; set; }
        [JsonPropertyName("EMP_Sexe")]
        public string? EMP_Sexe { get; set; }
        [JsonPropertyName("EMP_ROL_id")]
        public int EMP_ROL_id { get; set; }
        [JsonPropertyName("EMP_Email")]
        public string? EMP_Email { get; set; }
        [JsonPropertyName("EMP_Auth")]
        public string? EMP_Auth { get; set; }
        [JsonPropertyName("ROL_Libelle")]
        public string? ROL_Libelle { get; set; }
        [JsonPropertyName("EMP_Manager")]
        public string? EMP_Manager { get; set; }
        [JsonPropertyName("EMP_Manager_id")]
        public int EMP_Manager_id { get; set; }

        [JsonPropertyName("CON_id")]
        public int? CON_id { get; set; }
        [JsonPropertyName("CON_Type")]
        public string? CON_Type { get; set; }
        [JsonPropertyName("CON_JoursSemaine")]
        public int? CON_JoursSemaine { get; set; }
        [JsonPropertyName("CON_Description")]
        public string? CON_Description { get; set; }
        [JsonPropertyName("CON_DteDebut")]
        public DateTime? CON_DteDebut { get; set; }
        [JsonPropertyName("CON_DteFin")]
        public DateTime? CON_DteFin { get; set; }
        [JsonPropertyName("CON_EMP_id")]
        public int? CON_EMP_id { get; set; }
    }
    public class EmployeNomsDTO
    {
        [JsonPropertyName("EMP_id")]
        public int EMP_id { get; set; }
        [JsonPropertyName("EMP_Nom")]
        public string? EMP_Nom { get; set; }
    }
}
