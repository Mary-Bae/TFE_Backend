using Interfaces;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetUserAuth0Id(ClaimsPrincipal user)
        {
            //Trouvé sur le site auth0.com : https://community.auth0.com/t/getting-currently-logged-user-in-web-api/6810
            // A cet endroit : Getting currently logged user in web api

            var auth0Id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(auth0Id))
            {
                throw new UnauthorizedAccessException("Utilisateur non authentifié.");
            }
            return auth0Id;
        }
        public async Task<string> GetManagementApiToken() // Récupération du token du management Machine-to-Machine pour la création et l'update des users
        {
            using var client = new HttpClient();

            var payload = new
            {
                client_id = _configuration["Auth0Manage:ClientId"],
                client_secret = _configuration["Auth0Manage:ClientSecret"],
                audience = _configuration["Auth0Manage:Audience"],
                grant_type = "client_credentials"
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"https://{_configuration["Auth0Manage:Domain"]}/oauth/token", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var token = JsonDocument.Parse(json).RootElement.GetProperty("access_token").GetString();

            return token!;
        }
    }
}
