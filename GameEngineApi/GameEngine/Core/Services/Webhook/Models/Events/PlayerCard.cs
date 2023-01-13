namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class PlayerCard : WebhookEvent
	{
		public PlayerCard(string secret) : base(secret, Event.PlayerCards)
		{
			
		}
		public List<PlayerCard> Cards { get; set; }
	}
}
