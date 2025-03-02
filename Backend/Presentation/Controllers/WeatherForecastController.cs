using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        public ActionResult GetAll()
        {
            return Ok(new { Message = "Test du public" });
        }

        [Authorize]
        [HttpGet(Name = "GetAllPrivate")]
        public ActionResult GetAllPrivate()
        {
            return Ok(new { Message = "Test du privé" });
        }
    }
}
