using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public interface ICardManager
    {
        void GetPlayerHandValue(List<Card> playerCards, List<Card> boardCards);
    }
}
