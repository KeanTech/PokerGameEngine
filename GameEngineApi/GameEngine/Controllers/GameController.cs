using GameEngine.Core.Enums;
using GameEngine.Core.Managers;
using GameEngine.Core.Services.Webhook;
using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Data;
using GameEngine.Models.BusinessModels;
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

        public GameController(IWebhookService service, GameEngineContext ctx) 
        {
            _service = service;
            _context = ctx;
        }

        [HttpPost]
        [Route("Subscribe")]
        public IActionResult Subscribe(string callbackUrl, string userIdentifier, int tableId)
        {
	        // Make Webhook logic
            _service.Subscribe(callbackUrl, userIdentifier, tableId);
	        return Ok();
        }

        [HttpPost]
        [Route("Call")]
        public IActionResult Call()
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
        public IActionResult Raise(Raise playerEvent) 
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

        [HttpPost]
        [Route("DbTest")]
        public async Task TestData()
        {
			_context.Database.EnsureCreated();
			var cards = new List<Card>();
			foreach (Symbols symbol in Enum.GetValues(typeof(Symbols)))
			{
				foreach (CardTypes ct in Enum.GetValues(typeof(CardTypes)))
				{
					cards.Add(new Card(){Symbol = symbol, Type = ct});
				}
			}
			_context.Card.AddRange(cards);
			try
			{
				_context.SaveChanges();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

    }
}
