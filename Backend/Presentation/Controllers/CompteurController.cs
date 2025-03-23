using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;
using BusinessLayer;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompteurController : ControllerBase
    {
        private readonly ICompteurService _compteurService;
        private readonly IAuthService _authService;

        public CompteurController(ICompteurService compteurService, IAuthService authService)
        {
            _compteurService = compteurService;
            _authService = authService;
        }

       // [Authorize(Policy = "employee")]
        [HttpGet("GetCompteurByUser")]
        public async Task<IActionResult> GetCompteurByUser()
        {
            try
            {
                string auth0Id = _authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                var lst = await _compteurService.GetCompteurByUser<CompteurDTO>(auth0Id);
                return Ok(lst);
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
