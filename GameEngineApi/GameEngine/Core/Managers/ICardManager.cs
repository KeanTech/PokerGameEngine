using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public interface ICardManager
    {
        List<Player> GetPlayerHandValue(List<Card> playerCards, List<Card> boardCards);
    }
}
