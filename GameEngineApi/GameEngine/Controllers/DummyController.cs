using GameEngine.Core.Enums;
using GameEngine.Core.Managers;
using GameEngine.Data;
using GameEngine.Models.Events;
using GameEngine.Models.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DummyController : Controller
    {
        private readonly IGameManager _gameManager;
        private readonly GameEngineContext _context;
        private static PokerTable? _gameState;
        public DummyController(IGameManager gameManager, GameEngineContext context) 
        {
            _gameManager = gameManager;
            _context = context;
        }

        private bool DoesPlayerExistOnTable(GameState gameState, int playerId) 
        {
            Player? player = gameState.PokerTable.Players.FirstOrDefault(x => x.Id == playerId);
        
            if(player == null)
                return false;

            return true;
        }
        private bool IsValidGameState(GameState gameState) 
        {
            if(gameState == null)
                return false;

            if(gameState.PokerTable == null)
                return false;

            if(gameState.PokerTable.Id == 0)
                return false;

            if(gameState.PokerTable.Players == null)
                return false;

            if(gameState.PokerTable.Players.Count < 1)
                return false;

            return true;   
        }


        [HttpGet]
        [Route("CreateNewGame")]
        public IActionResult Index()
        {
            PokerTable pokerTable = new PokerTable()
            {
                Players = new List<Player>()
                {
                    new Player() { Name = "Player1", ChipsValue = 500 },
                    new Player() { Name = "Player2", ChipsValue = 500 }
                },
                CardDeck = new List<DeckCard>() 
                {
                    new DeckCard() {  }
                }
            };

            if (_gameState == null)
                _gameState = _gameManager.StartNewGame(pokerTable).Result;

            _gameManager.GiveCardsToPlayers(pokerTable);
            _gameManager.GiveCardsToTable(3, pokerTable);

            return Ok(_gameState);
        }

        [HttpPut]
        [Route("GiveCards")]
        public IActionResult GiveCards(GameState gameState) 
        {
           //GameState newGameState = _gameManager.GiveCards(1, gameState);
           return Ok();
        }

        [HttpPut]
        [Route("Call")]
        public IActionResult Call(BetEvent betEvent) 
        {
            //GameState gameState = new GameState(); /// Get gameState from service with <see cref="BetEvent.PokerTableId"/>

            if (IsValidGameState(_gameState) == false)
                return BadRequest();

            if (DoesPlayerExistOnTable(_gameState, betEvent.PlayerId))
                return NotFound();



            return Ok();
        }

        [HttpPut]
        [Route("Fold")]
        public IActionResult Fold(int playerId) 
        {
            return Ok();
        }

        [HttpPut]
        [Route("Raise")]
        public IActionResult Raise(BetEvent betEvent) 
        {
            GameState gameState = new GameState(); /// Get gameState from service with <see cref="BetEvent.PokerTableId"/>

            if (IsValidGameState(gameState) == false)
                return BadRequest();
            
            if (DoesPlayerExistOnTable(gameState, betEvent.PlayerId))
                return NotFound();
            
            
            return Ok();
        }

        [HttpPut]
        [Route("AllIn")]
        public IActionResult AllIn(BetEvent betEvent) 
        {
            GameState gameState = new GameState(); /// Get gameState from service with <see cref="BetEvent.PokerTableId"/>

            if (IsValidGameState(gameState) == false)
                return BadRequest();

            if (DoesPlayerExistOnTable(gameState, betEvent.PlayerId))
                return NotFound();

            return Ok();
        }

        [HttpPut]
        [Route("Check")]
        public IActionResult Check(int playerId, string userIdentifier) 
        {
            return Ok();
        }
    }
}
