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

        //[Authorize(Policy = "employee")]
        [HttpGet("GetDemandes")]
        public async Task<ActionResult> GetDemandes()
        {
            try
            {
                List<DemandesDTO> lst;

                IDemandesService demandeSvc = _demandesService;
                lst = await demandeSvc.GetDemandes<DemandesDTO>();
                return Ok(lst);
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




        [HttpGet("GetPublic")]
        public ActionResult GetPublic()
        {
            return Ok(new { Message = "Test du public" });
        }

        [Authorize(Policy = "employee")]
        [HttpGet("GetPrivateEmployee")]
        public ActionResult GetPrivateEmployee()
        {
            return Ok(new { Message = "Test du privé de l'employé" });
        }

        [Authorize]
        [HttpGet("GetPrivate")]
        public ActionResult GetPrivate()
        {
            return Ok(new { Message = "Test du privé" });
        }

        [Authorize(Policy = "administrator")]
        [HttpGet("GetPrivateAdmin")]
        public ActionResult GetPrivateAdmin()
        {
            return Ok(new { Message = "Test du privé de l'admin" });
        }
    }
}
