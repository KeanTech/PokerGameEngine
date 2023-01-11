using GameEngine.Core.Enums;
using GameEngine.Core.Managers;
using GameEngine.Models.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DummyController : Controller
    {
        private readonly GameManager _gameManager;

        public DummyController(GameManager gameManager) 
        {
            _gameManager = gameManager;
        }

        [HttpGet]
        [Route("CreateNewGame")]
        public IActionResult Index()
        {
            var newGame = _gameManager.StartNewGame(1);

            return Ok(newGame);
        }
    }
}
