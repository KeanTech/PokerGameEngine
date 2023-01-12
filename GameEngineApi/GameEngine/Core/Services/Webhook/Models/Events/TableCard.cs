using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class TableCard : WebhookEvent
	{
		public List<Card> Cards { get; set; }
		public TableCard(List<Card> cards, string secret) : base(secret, Event.TableCards)
		{
			Cards = cards;
		}
	}
}
