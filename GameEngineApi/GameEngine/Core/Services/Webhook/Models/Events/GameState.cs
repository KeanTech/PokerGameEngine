using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class GameState : WebhookEvent
	{
		public GameState(int currentBet, int chipsPool, int currentPlayer, List<Card> tableCards, List<Player> players, string secret) : base(secret, Event.GameState)
		{
			CurrentBet = currentBet;
			ChipsPoolAmount = chipsPool;
			Players = players;
			TableCards = tableCards;
			CurrentTurnPlayerId = currentPlayer;
		}

		public int CurrentBet { get; set; }
		public int ChipsPoolAmount { get; set; }
		public int CurrentTurnPlayerId { get; set; }
		public List<Card> TableCards { get; set; }
		public List<Player> Players { get; set; }
	}
}
