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
            return Ok(new { Message = "Test du privé" });
        }
    }
}
