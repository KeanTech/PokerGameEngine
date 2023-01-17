using GameEngine.Core.Enums;
using GameEngine.Core.Managers;
using GameEngine.Core.Services.Webhook;
using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Data;
using GameEngine.Models.Events;
using GameEngine.Models.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly GameManager _gameManager;
        private readonly IWebhookService _service;
        private readonly GameEngineContext _context;

        public GameController(IWebhookService service, GameEngineContext context) 
        {
            _service = service;
            _context = context;
        }

        [HttpPut]
        [Route("StartNewGame")]
        public IActionResult StartNewGame(IList<User> users) 
        {
            foreach (var user in users)
            {
               
            }

            //PokerTable pokerTable = _gameManager.StartNewGame().Result;

            return Ok();
        }

        [HttpPut]
        [Route("Subscribe")]
        public IActionResult Subscribe(string callbackUrl, string userIdentifier, int tableId)
        {
	        // Make Webhook logic
            _service.Subscribe(callbackUrl, userIdentifier, tableId);
	        return Ok();
        }

        [HttpPut]
        [Route("Call")]
        public IActionResult Call(BetEvent betEvent) 
        {
            if (betEvent.PokerTableId == 0)
                return NotFound();

            if (betEvent.PlayerId == 0)
                return NotFound();

            if (string.IsNullOrEmpty(betEvent.PlayerIdentifier))
                return BadRequest();

            if (betEvent.BetAmount == 0)
                return BadRequest();

            _gameManager.PlayerCall(betEvent);

            return Ok();
        }

        [HttpPut]
        [Route("Fold")]
        public IActionResult Fold(TurnEvent turnEvent) 
        {
            if (turnEvent.PokerTableId == 0)
                return NotFound();

            if (turnEvent.PlayerId == 0)
                return NotFound();

            if (string.IsNullOrEmpty(turnEvent.PlayerIdentifier))
                return BadRequest();

            _gameManager.PlayerFold(turnEvent);

            return Ok();
        }

        [HttpPut]
        [Route("Raise")]
        public IActionResult Raise(BetEvent betEvent) 
        {
            if (betEvent.PokerTableId == 0)
                return NotFound();

            if (betEvent.PlayerId == 0)
                return NotFound();

            if (string.IsNullOrEmpty(betEvent.PlayerIdentifier))
                return BadRequest();

            if (betEvent.BetAmount == 0)
                return BadRequest();

            _gameManager.PlayerRaise(betEvent);

            return Ok();   
        }

        [HttpPut]
        [Route("Check")]
        public IActionResult Check(TurnEvent turnEvent) 
        {
            if (turnEvent.PokerTableId == 0)
                return NotFound();

            if (turnEvent.PlayerId == 0)
                return NotFound();

            if (string.IsNullOrEmpty(turnEvent.PlayerIdentifier))
                return BadRequest();

            _gameManager.PlayerCheck(turnEvent);

            return Ok();
        }

        [HttpPut]
        [Route("AllIn")]
        public IActionResult AllIn(BetEvent betEvent) 
        {
            if (betEvent.PokerTableId == 0)
                return NotFound();

            if (betEvent.PlayerId == 0)
                return NotFound();

            if (string.IsNullOrEmpty(betEvent.PlayerIdentifier))
                return BadRequest();

            if (betEvent.BetAmount == 0)
                return BadRequest();

            _gameManager.PlayerRaise(betEvent);

            return Ok();
        }
    }
}
