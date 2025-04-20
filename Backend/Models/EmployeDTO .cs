using Microsoft.VisualBasic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class EmployeDTO
    {
        [JsonPropertyName("EMP_Nom")]
        public string? EMP_Nom { get; set; }
        [JsonPropertyName("EMP_Prenom")]
        public string? EMP_Prenom { get; set; }
        [JsonPropertyName("EMP_Email")]
        public string? EMP_Email { get; set; }
    }
}
