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
        public IActionResult StartNewGame(int tableId)
        {
            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == tableId);
            if (pokerTable == null)
                return NotFound();

            pokerTable = _gameManager.StartNewGame(pokerTable, 500).Result;

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
            if(pokerTable == null)
                return NotFound();

            Player player = new Player() { Table = pokerTable, User = user, Cards = new List<Card>() }; 
            pokerTable.Players.Add(player);
           
            _context.SaveChanges();

            // Subscrib user to webhook
            //Subscribe();
            return Ok();
        }

        [HttpPost]
        [Route("CreateTable")]
        public IActionResult CreateTable(User inputUser)
        {
            PokerTable pokerTable = new PokerTable();
            // create card deck in db if theres non
            if (_context.Card.Count() == 0)
            {
                _context.Card.AddRange(GameManager.GetNewCardDeck());
                _context.SaveChanges();
            }

            if (pokerTable.Deck == null)
                pokerTable.Deck = new Deck();

            if (pokerTable.Deck.Cards == null)
                pokerTable.Deck.Cards = new List<Card>();

            pokerTable.Deck.Cards = _context.Card.ToList();
            var user = _context.User.FirstOrDefault(x => x.Id == inputUser.Id);

            Player player = new Player();
            if (user != null)
            { 
                player = new Player() { User = user, Cards = new List<Card>() };
                _context.Player.Add(player);       
                _context.SaveChanges();
                pokerTable.Owner = player;
                pokerTable.OwnerId = player.Id;
            }

            _context.Table.Add(pokerTable);
            _context.SaveChanges();

            player.Table= pokerTable;
            _context.Update(player);
            _context.SaveChanges();

            // subscrib to webhook (user)

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
