using Microsoft.AspNetCore.Mvc;

namespace Log_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetErrors()
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult GetWarnings()
        {
            return Ok();
        }
    }
}