using Microsoft.VisualBasic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class ManagerDTO
    {
        [JsonPropertyName("EMP_Email")]
        public string? EMP_Email { get; set; }
    }
}
