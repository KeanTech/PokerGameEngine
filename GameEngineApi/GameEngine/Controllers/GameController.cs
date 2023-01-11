using GameEngine.Core.Managers;
using GameEngine.Models.Events;
using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly GameManager _gameManager;

        public GameController(GameManager gameManager) 
        {
            _gameManager = gameManager;
        }

        [HttpPost]
        [Route("Call")]
        public IActionResult Call(PlayerEvent playerEvent) 
        {
            // Make Webhook logic
            return Ok();
        }

        [HttpPost]
        [Route("Fold")]
        public IActionResult Fold() 
        {
            return Ok();
        }

        [HttpPost]
        [Route("Raise")]
        public IActionResult Raise() 
        {
            return Ok();   
        }

        [HttpPost]
        [Route("Check")]
        public IActionResult Check() 
        {
            return Ok();
        }

        [HttpPost]
        [Route("AllIn")]
        public IActionResult AllIn() 
        {
            return Ok();
        }

    }
}
