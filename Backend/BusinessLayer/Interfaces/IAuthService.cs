using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IAuthService
    {
        string GetUserAuth0Id(ClaimsPrincipal user);
        Task<string> GetManagementApiToken();
    }
}
