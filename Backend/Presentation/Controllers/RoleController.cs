using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Models;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [Authorize(Policy = "administrator")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {       
                var lst = await _roleService.GetRoles<RoleDTO>();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
