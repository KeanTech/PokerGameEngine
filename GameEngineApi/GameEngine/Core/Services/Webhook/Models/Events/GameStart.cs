using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook.Models.Events
{
    public class GameStart : StateEvent
    {
        IList<Card> PlayerCards { get; set; }
        public GameStart(string secret, PokerTable table, IList<Card> playerCards) : base(secret, Event.RoundEnd, table)
        {
            PlayerCards = playerCards;
        }
    }
}
