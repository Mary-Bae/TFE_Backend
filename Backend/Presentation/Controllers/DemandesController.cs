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
        private readonly IAuthService _authService;

        public DemandesController(IDemandesService demandesService, IAuthService authService)
        {
            _demandesService = demandesService;
            _authService = authService;
        }

        [Authorize (Policy = "employee")]
        [HttpGet("GetDemandesByUser")]
        public async Task<IActionResult> GetDemandesByUser()
        {
            try
            {
                string auth0Id = _authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                Console.WriteLine($"Auth0Id récupéré : {auth0Id}");
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
        public async Task<IActionResult> GetTypeAbsByUser()
        {
            try
            {
                string auth0Id = _authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                Console.WriteLine($"Auth0Id récupéré : {auth0Id}");
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
        public async Task<IActionResult> AjoutDemandeAbsence(AddAndUpdDemandeDTO ajoutDemande)
        {
            try
            {
                string auth0Id = _authService.GetUserAuth0Id(User);
                await _demandesService.AddDemandeAbs(ajoutDemande, auth0Id);
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
                await _demandesService.UpdateDemande(id, majDemande);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "employee")]
        [HttpDelete("DelDemande")]
        public async Task<IActionResult> DeleteDemande(int id)
        {
            try
            {
                await _demandesService.DeleteDemande(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
