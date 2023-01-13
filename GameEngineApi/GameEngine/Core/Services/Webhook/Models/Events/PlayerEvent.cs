namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class PlayerEvent : WebhookEvent
	{
		public PlayerEvent(string secret, Event eventType, int playerId) : base(secret, eventType)
		{
			PlayerId = playerId;
		}
		public int PlayerId { get; set; }
	}
}
