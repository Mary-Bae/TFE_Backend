using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Interfaces;

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
        public IEnumerable<Demandes> GetDemandes()
        {
            return _demandesService.GetDemandes();
        }

        [HttpPost("AddDemande")]
        public IActionResult Post(Demandes demande)
        {
            if (demande == null)
            {
                return BadRequest("La demande est invalide.");
            }
            _demandesService.Add(demande);

            return Ok(new { Message = "Demande ajout�e avec succ�s" });
        }




        [HttpGet("GetPublic")]
        public ActionResult GetPublic()
        {
            return Ok(new { Message = "Test du public" });
        }

        [Authorize(Policy = "employee")]
        [HttpGet("GetPrivateEmployee")]
        public ActionResult GetPrivateEmployee()
        {
            return Ok(new { Message = "Test du priv� de l'employ�" });
        }

        [Authorize]
        [HttpGet("GetPrivate")]
        public ActionResult GetPrivate()
        {
            return Ok(new { Message = "Test du priv�" });
        }

        [Authorize(Policy = "administrator")]
        [HttpGet("GetPrivateAdmin")]
        public ActionResult GetPrivateAdmin()
        {
            return Ok(new { Message = "Test du priv� de l'admin" });
        }
    }
}
