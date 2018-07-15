using Microsoft.AspNetCore.Mvc;

namespace Ford.Tracker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Ping")]
    public class PingController : Controller
    {
        public IActionResult Get()
        {
            return Ok("Pong");
        }
    }
}