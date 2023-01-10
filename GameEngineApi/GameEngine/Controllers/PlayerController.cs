using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        [HttpPost]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok();
        }
    }
}
