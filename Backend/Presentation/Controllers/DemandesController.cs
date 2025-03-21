using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Interfaces;
using Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemandesController : ControllerBase
    {
        private readonly IDemandesService _demandesService;

        public DemandesController(IDemandesService demandesService)
        {
            _demandesService = demandesService;
        }

        [Authorize (Policy = "employee")]
        [HttpGet("GetDemandesByUser")]
        public async Task<ActionResult> GetDemandesByUser([FromServices] IAuthService authService)
        {
            try
            {
                string auth0Id = authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                var lst = await _demandesService.GetDemandesByUser<DemandesDTO>(auth0Id);
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
        [Authorize(Policy = "employee")]
        [HttpGet("GetTypeAbsByUser")]
        public async Task<ActionResult> GetTypeAbsByUser([FromServices] IAuthService authService)
        {
            try
            {
                string auth0Id = authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                var lst = await _demandesService.GetTypeAbsByUser<TypeAbsenceDTO>(auth0Id);
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
        [Authorize(Policy = "employee")]
        [HttpPost("AjoutDemandeAbsence")]
        public async Task<IActionResult> AjoutDemandeAbsence(AddDemandeDTO ajoutDemande, [FromServices] IAuthService authService)
        {
            try
            {
                IDemandesService demande = _demandesService;
                string auth0Id = authService.GetUserAuth0Id(User);
                await demande.AddDemandeAbs(ajoutDemande, auth0Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Policy = "employee")]
        //[HttpGet("GetDemandeById")]
        //public IActionResult GetDemandeById(int id)
        //{
        //    Demandes demande = _demandesService.GetDemandeById(id);

        //    if (demande == null)
        //    {
        //        return NotFound($"Aucune demande trouvée avec l'ID {id}");
        //    }

        //    return Ok(demande);
        //}


    }
}
