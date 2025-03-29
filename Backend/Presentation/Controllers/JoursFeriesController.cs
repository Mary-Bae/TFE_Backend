using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JoursFeriesController : ControllerBase
    {
        private readonly IJoursFeriesService _joursFeriesService;

        public JoursFeriesController(IJoursFeriesService joursFeriesService)
        {
            _joursFeriesService = joursFeriesService;
        }
        [HttpGet("GetJoursFeries")]
        public async Task<IActionResult> GetJoursFeries()
        {
            try
            {       
                var lst = await _joursFeriesService.GetJoursFeries<JoursFeriesDTO>();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
