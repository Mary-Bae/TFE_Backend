using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetDemandesByUser(IAuthService authService)
        {
            try
            {
                string auth0Id = authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                var lst = await _demandesService.GetDemandesByUser<GetDemandesDTO>(auth0Id);
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
        [HttpGet("GetDemandeById")]
        public async Task<IActionResult?> GetDemandeById(int id)
        {
            try
            {
                var demande = await _demandesService.GetDemandeById<DemandesByIdDTO>(id);
                if (demande == null)
                {
                    return NotFound($"Aucune demande trouvée avec l'ID {id}");
                }

                return Ok(demande);
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
        public async Task<IActionResult> GetTypeAbsByUser(IAuthService authService)
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
        public async Task<IActionResult> AjoutDemandeAbsence(AddAndUpdDemandeDTO ajoutDemande, IAuthService authService)
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
        [Authorize(Policy = "employee")]
        [HttpPut("MajDemande")]
        public async Task<IActionResult> MajDemande(int id, AddAndUpdDemandeDTO majDemande)
        {
            try
            {
                IDemandesService demande = _demandesService;
                await demande.UpdateDemande(id, majDemande);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
