using GameEngine.Core.Services.Webhook.Models.Events;

namespace GameEngine.Core.Services.Webhook
{
	public interface IWebhookService
	{
		Task NotifySubscriberOfStateEvent(string userIdentifier, int tableId, WebhookEvent eventData);
		Task NotifySubscribersOfPlayerEvent(PlayerEvent eventData, int tableId);
		void Subscribe(string callbackUrl, string userSecret, int tableId);
	}
}
