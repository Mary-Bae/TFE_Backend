using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;
using BusinessLayer;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeController : ControllerBase
    {
        private readonly IEmployeService _employeService;
        private readonly IAuthService _authService;

        public EmployeController(IEmployeService employeService, IAuthService authService)
        {
            _employeService = employeService;
            _authService = authService;
        }

        [Authorize (Policy = "employee")]
        [HttpGet("GetMailManagerByUser")]
        public async Task<IActionResult> GetMailManagerByUser()
        {
            try
            {
                string auth0Id = _authService.GetUserAuth0Id(User); // Récupérer l’ID Auth0
                var eMail = await _employeService.GetMailManagerByUser<EmployeDTO>(auth0Id);
                return Ok(eMail);
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
        [Authorize(Policy = "Manager")]
        [HttpGet("GetMailByDemande")]
        public async Task<IActionResult> GetMailByDemande(int demId)
        {
            try
            {
                var eMail = await _employeService.GetMailByDemande<EmployeDTO>(demId);
                return Ok(eMail);
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
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _employeService.GetUsers<EmployeDTO>();
                return Ok(users);
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
        [HttpGet("GetDeletedUsers")]
        public async Task<IActionResult> GetDeletedUsers()
        {
            try
            {
                var users = await _employeService.GetDeletedUsers<EmployeDTO>();
                return Ok(users);
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
        [HttpGet("GetManagers")]
        public async Task<IActionResult> GetManagers()
        {
            try
            {
                var lst = await _employeService.GetManagers<EmployeNomsDTO>();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "administrator")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(EmployeDTO employe)
        {
            try
            {
                var bearerToken = Request.Headers["Authorization"].ToString();
                var createdUser = await _employeService.CreateUser(employe);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "administrator")]
        [HttpPut("UpdateEmploye")]
        public async Task<IActionResult> UpdateEmploye(int id, EmployeDTO employe)
        {
            try
            {
                var bearerToken = Request.Headers["Authorization"].ToString();
                var employeUpdated = await _employeService.UpdateEmploye(id, employe);
                return Ok(employeUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "administrator")]
        [HttpGet("GetEmployeById")]
        public async Task<IActionResult?> GetEmployeById(int id)
        {
            try
            {
                var employe = await _employeService.GetEmployeById<EmployeDTO>(id);
                if (employe == null)
                {
                    return NotFound($"Aucun employe trouvée avec l'ID {id}");
                }

                return Ok(employe);
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
        [HttpPut("DelEmploye")]
        public async Task<IActionResult> DeleteEmploye(int id)
        {
            try
            {
                await _employeService.DeleteEmploye(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "administrator")]
        [HttpPut("RestoreEmploye")]
        public async Task<IActionResult> RestaureEmploye(int id)
        {
            try
            {
                await _employeService.RestoreEmploye(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
