using Interfaces;
using System.Security.Claims;

namespace BusinessLayer
{
    public class AuthService : IAuthService
    {
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
    }
}
