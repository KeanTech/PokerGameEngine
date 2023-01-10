using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public interface ICardManager
    {
        void ValidatePlayerHand(List<Card> playerHand, List<Card> cardsOnBoard);
    }
}
