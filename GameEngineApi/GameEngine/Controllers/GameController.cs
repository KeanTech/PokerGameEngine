using GameEngine.Core.Managers;
using GameEngine.Core.Services.Webhook;
using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Data;
using GameEngine.Models.Events;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Call(BetEvent betEvent) 
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

    }
}
