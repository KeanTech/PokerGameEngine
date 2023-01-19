using GameEngine.Core.Managers;
using GameEngine.Core.Services.Webhook;
using GameEngine.Core.Services.Webhook.Models;
using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Data;
using GameEngine.Models.Events;
using GameEngine.Models.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Reflection;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly GameManager _gameManager;
        private readonly IWebhookService _service;
        private readonly GameEngineContext _context;

        public GameController(IWebhookService service, GameEngineContext context, GameManager gameManager)
        {
            _service = service;
            _context = context;
            _gameManager = gameManager;
        }

        [HttpPut]
        [Route("StartNewGame")]
        public IActionResult StartNewGame(int tableId, int startingChipAmount)
        {
            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == tableId);
            if (pokerTable == null)
                return NotFound();

            if (startingChipAmount == 0)
                return BadRequest();

            pokerTable = _gameManager.StartNewGame(pokerTable.Id, startingChipAmount).Result;

            
            return Ok();
        }

        [HttpPut]
        [Route("SetPlayerReady")]
        public IActionResult SetPlayerReady(int userId, int tableId)
        {
            User? user = _context.User.FirstOrDefault(x => x.Id == userId);

            if (user == null)
                return NotFound();

            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == tableId);

            if (pokerTable == null)
                return NotFound();

            PlayerEvent playerEvent = new PlayerEvent(user.UserSecret, Event.PlayerReady, user.Id);
            _service.NotifySubscribersOfPlayerEvent(playerEvent, tableId);

            return Ok();
        }

        [HttpPut]
        [Route("JoinTable")]
        public IActionResult JoinTable(int userId, int tableId)
        {
            User? user = _context.User.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return NotFound();

            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == tableId);
            if (pokerTable == null)
                return NotFound();

            bool success = _gameManager.JoinTable(user, pokerTable);

            if(success)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("CreateTable")]
        public IActionResult CreateTable(int userId)
        {
            User? user = _context.User.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return NotFound();

            bool result = _gameManager.CreateNewPokerTable(user);

            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpPut]
        [Route("LeaveTable")]
        public IActionResult LeaveTable(int playerId) 
        {


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
