using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
        public class RoleDTO
        {
            [JsonPropertyName("ROL_id")]
            public int ROL_id { get; set; }
            [JsonPropertyName("ROL_Libelle")]
            public string? ROL_Libelle { get; set; }
        }
}
