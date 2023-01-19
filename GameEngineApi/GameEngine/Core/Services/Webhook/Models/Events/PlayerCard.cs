using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class PlayerCard : WebhookEvent
	{
		public PlayerCard(string secret, List<Card> cards) : base(secret, Event.PlayerCards)
		{
			Cards = cards;
		}
		public List<Card> Cards { get; set; }
	}
}
