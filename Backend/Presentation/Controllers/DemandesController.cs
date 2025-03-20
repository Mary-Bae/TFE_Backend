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

        //[HttpPost("AddDemande")]
        //public IActionResult Post(Demandes demande)
        //{
        //    if (demande == null)
        //    {
        //        return BadRequest("La demande est invalide.");
        //    }
        //    _demandesService.Add(demande);

        //    return Ok(new { Message = "Demande ajoutée avec succès" });
        //}
    }
}
