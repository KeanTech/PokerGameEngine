using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebHookService;
using WebHookService.Models;
using WebHookService.Models.Events;

namespace GameEngine.Controllers
{
    [ApiController]
    public class GameController : Controller
    {
	    private WebHookService.WebhookService _service;
	    public GameController(WebHookService.WebhookService service)
	    {
		    _service = service;
	    }

	    [HttpPost]
        [Route("api/Subscribe")]
        public IActionResult Subscribe(string callbackUrl, int tableId, string userIdentifier)
        { ;
            _service.Subscribe(callbackUrl, userIdentifier, tableId);
            return Ok();
        }

        [HttpPost]
        [Route("api/AddText")]
        public async Task<IActionResult> AddText(int tableId)
        {
	        var json = JsonConvert.SerializeObject(new PlayerEvent("dmskaldmsa", Event.Fold, 32));
	        await _service.NotifySubscribers(new PlayerEvent("dmsakldmsad", Event.Fold, 32), tableId);
	        await _service.NotifySubscribers(new Call(200, 3000, 32, "dsmakldsalkdamdlak"), tableId);
            return Ok();
        }

        [HttpPost]
        [Route("api/Call")]
        public IActionResult Call([FromBody]Call eventData)
        {
	        return Ok(eventData);
        }

        [HttpPost]
        [Route("api/Fold")]
        public IActionResult Fold([FromBody] PlayerEvent eventData)
        {
	        return Ok(eventData);
        }

        [HttpPost]
        [Route("api/Check")]
        public IActionResult Check([FromBody] PlayerEvent eventData)
        {
	        return Ok(eventData);
        }

		[HttpPost]
        [Route("api/AllIn")]
        public IActionResult AllIn([FromBody] AllIn eventData)
        {
	        return Ok(eventData);
        }
	}
}
