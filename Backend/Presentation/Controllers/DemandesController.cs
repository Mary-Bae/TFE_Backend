using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandesController : ControllerBase
    {
        private readonly IDemandesService _demandesService;

        public DemandesController(IDemandesService demandesService)
        {
            _demandesService = demandesService;
        }

        [Authorize(Policy = "employee")]
        [HttpGet("GetDemandes")]
        public IEnumerable<Demandes> GetDemandes()
        {
            return _demandesService.GetDemandes();
        }






        [HttpGet("GetPublic")]
        public ActionResult GetPublic()
        {
            return Ok(new { Message = "Test du public" });
        }


        [HttpPost("Demandes")]
        //public IActionResult Post(Demandes demande)
        //{
        //    if (demande == null)
        //    {
        //        return BadRequest("La demande est invalide.");
        //    }

        //    _demandesService.Add(demande);

        //    return Ok(new { Message = "Demande ajout�e avec succ�s" });
        //}

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
