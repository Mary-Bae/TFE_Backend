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
        [JsonPropertyName("EMP_Email")]
        public string? EMP_Email { get; set; }
        [JsonPropertyName("EMP_Auth")]
        public string? EMP_Auth { get; set; }
        [JsonPropertyName("ROL_Libelle")]
        public string? ROL_Libelle { get; set; }
        [JsonPropertyName("EMP_Manager")]
        public string? EMP_Manager { get; set; }
    }
}
