﻿using GameEngine.Core.Enums;
using GameEngine.Core.Managers;
using GameEngine.Models.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace GameEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DummyController : Controller
    {
        private readonly IGameManager _gameManager;

        public DummyController(IGameManager gameManager) 
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

        [HttpPut]
        [Route("GiveCards")]
        public IActionResult GiveCards(GameState gameState) 
        {
           GameState newGameState = _gameManager.GiveCards(1, gameState);
           return Ok(newGameState);
        }

        [HttpPut]
        [Route("Call")]
        public IActionResult Call( int playerId, int callAmount) 
        {
            return Ok();
        }

        [HttpPut]
        [Route("Fold")]
        public IActionResult Fold() 
        {
            return Ok();
        }

        [HttpPut]
        [Route("Raise")]
        public IActionResult Raise() 
        {
            return Ok();
        }

    }
}
