using Interfaces;
using System.Security.Claims;

namespace BusinessLayer
{
    public class AuthService : IAuthService
    {
        public string GetUserAuth0Id(ClaimsPrincipal user)
        {
            var auth0Id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(auth0Id))
            {
                throw new UnauthorizedAccessException("Utilisateur non authentifié.");
            }
            return auth0Id;
        }
    }
}
