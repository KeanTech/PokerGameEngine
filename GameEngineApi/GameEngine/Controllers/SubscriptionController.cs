using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        [HttpGet]
        [Route("Subcribe")]
        public IActionResult Subscrib()
        {
            return Ok();
        }
    }
}
