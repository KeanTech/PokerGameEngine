using RestSharp;
using WebHookService.Models;
using WebHookService.Models.Events;

namespace WebHookService
{
	public class WebhookService
	{
		private WebhookContext _context;
		private RestClient _client;

		public WebhookService(WebhookContext context, RestClient client)
		{
			_client = client;
			_context = context;
		}

		public void Subscribe(string callbackUrl, string userIdentifier, int tableId)
		{
			_context.Database.EnsureCreated();
			_context.Subscribe.Add(new SubscribeModel()
				{ CallbackUrl = callbackUrl, TableId = tableId, UserIdentifier = userIdentifier });
			_context.SaveChanges();
		}

		public List<SubscribeModel> GetSubscribers(int tableId)
		{
			return _context.Subscribe.Where(e => e.TableId.Equals(tableId)).ToList();
		}

		public async Task NotifySubscribers(WebhookEvent eventData, int tableId)
		{
			var subs = GetSubscribers(tableId);
			foreach (var sub in subs)
			{
				var request = new RestRequest(sub.CallbackUrl + $"/{eventData.EventType}");
				request.AddJsonBody(eventData);
				await _client.PostAsync(request);
			}
		}
	}
}