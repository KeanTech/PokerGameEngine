using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class RoundEnd : StateEvent
	{
		public IList<Player> Winners { get; set; }
		public int WinningPool { get; set; }
		public RoundEnd(string secret, PokerTable table) : base(secret, Event.RoundEnd, table)
		{

		}
	}
}
