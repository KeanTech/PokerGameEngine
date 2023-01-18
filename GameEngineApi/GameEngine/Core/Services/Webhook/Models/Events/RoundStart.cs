using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook.Models.Events
{
    public class RoundStart : StateEvent
    {
        public RoundStart(string secret, PokerTable table) : base(secret, Event.RoundStart, table)
        {

        }
    }
}
