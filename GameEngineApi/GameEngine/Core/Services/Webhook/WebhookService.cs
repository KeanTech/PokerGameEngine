using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Data;
using RestSharp;

namespace GameEngine.Core.Services.Webhook
{
	public class WebhookService : IWebhookService
	{
		private GameEngineContext _context;
		private RestClient _client;

		public WebhookService(GameEngineContext context)
		{
			_client = new RestClient();
			_context = context;
		}

		public void Subscribe(string callbackUrl, string userIdentifier, int tableId)
		{
			_context.Database.EnsureCreated();
			_context.Subscribe.Add(new SubscribeModel()
				{ CallbackUrl = callbackUrl, TableId = tableId, UserSecret = userIdentifier });
			_context.SaveChanges();
		}

		private List<SubscribeModel> GetSubscribers(int tableId)
		{
			return _context.Subscribe.Where(e => e.TableId.Equals(tableId)).ToList();
		}

		public async Task NotifySubscriberOfStateEvent(string userIdentifier, int tableId, WebhookEvent eventData)
		{
			var sub = _context.Subscribe.Where(e => e.UserSecret.Equals(userIdentifier) && e.TableId.Equals(tableId)).ToList();
			var request = new RestRequest(sub.FirstOrDefault()?.CallbackUrl + $"/{eventData.EventType}");
			await _client.PostAsync(request);
		}

		public async Task NotifySubscribersOfPlayerEvent(PlayerEvent eventData, int tableId)
		{
			var subs = GetSubscribers(tableId);
			var tasks = new List<Task>();
			foreach (var sub in subs)
			{
				async Task func()
				{
					var request = new RestRequest(sub.CallbackUrl + $"/{eventData.EventType}");
					request.AddJsonBody(eventData);
					await _client.PostAsync(request);
				}
				tasks.Add(func());
			}
			await Task.WhenAll(tasks);
		}
	}
}