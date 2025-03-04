using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("GetPublic")]
        public ActionResult GetPublic()
        {
            return Ok(new { Message = "Test du public" });
        }

        [Authorize]
        [HttpGet("GetPrivate")]
        public ActionResult GetPrivate()
        {
            return Ok(new { Message = "Test du priv�" });
        }
        [Authorize(Policy = "employee")]
        [HttpGet("GetPrivateEmployee")]
        public ActionResult GetPrivateEmployee()
        {
            return Ok(new { Message = "Test du priv� de l'employ�" });
        }
        [Authorize(Policy = "administrator")]
        [HttpGet("GetPrivateAdmin")]
        public ActionResult GetPrivateAdmin()
        {
            return Ok(new { Message = "Test du priv� de l'admin" });
        }
    }
}
