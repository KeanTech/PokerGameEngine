using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class StateEvent : WebhookEvent
	{
		PokerTable PokerTable { get; set; }
		public StateEvent(string secret, Event eventType, PokerTable table) : base(secret, eventType)
		{
			PokerTable = table;
		}
	}
}
