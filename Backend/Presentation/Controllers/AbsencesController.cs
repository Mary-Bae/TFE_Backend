using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsencesService _absencesService;

        public AbsencesController(IAbsencesService absencesService)
        {
            _absencesService = absencesService;
        }
        [Authorize(Policy = "administrator")]
        [HttpGet("GetAbsences")]
        public async Task<IActionResult> GetAbsences()
        {
            try
            {
                var absences = await _absencesService.GetAbsences<AbsenceDTO>();
                return Ok(absences);
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
        [Authorize(Policy = "administrator")]
        [HttpGet("GetAbsencesByEmployeId")]
        public async Task<IActionResult> GetAbsencesByEmployeId(int employeId)
        {
            try
            {
                var absences = await _absencesService.GetAbsencesByEmployeId<TypeAbsenceDTO>(employeId);
                return Ok(absences);
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
