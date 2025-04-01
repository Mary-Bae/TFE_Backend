using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly IAuthService _authService;

        public ManagerController(IManagerService managerService, IAuthService authService)
        {
            _managerService = managerService;
            _authService = authService;
        }

        [Authorize (Policy = "employee")]
        [HttpGet("GetMailManagerByUser")]
        public async Task<IActionResult> GetMailManagerByUser()
        {
            try
            {
                string auth0Id = _authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                var eMail = await _managerService.GetMailManagerByUser<ManagerDTO>(auth0Id);
                return Ok(eMail);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
