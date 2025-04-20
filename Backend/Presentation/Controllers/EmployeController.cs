using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeController : ControllerBase
    {
        private readonly IEmployeService _employeService;
        private readonly IAuthService _authService;

        public EmployeController(IEmployeService employeService, IAuthService authService)
        {
            _employeService = employeService;
            _authService = authService;
        }

        [Authorize (Policy = "employee")]
        [HttpGet("GetMailManagerByUser")]
        public async Task<IActionResult> GetMailManagerByUser()
        {
            try
            {
                string auth0Id = _authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                var eMail = await _employeService.GetMailManagerByUser<EmployeDTO>(auth0Id);
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
        [Authorize(Policy = "Manager")]
        [HttpGet("GetMailByDemande")]
        public async Task<IActionResult> GetMailByDemande(int demId)
        {
            try
            {
                var eMail = await _employeService.GetMailByDemande<EmployeDTO>(demId);
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
