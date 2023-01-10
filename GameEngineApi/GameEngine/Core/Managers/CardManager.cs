using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class CardManager : ICardManager
    {
        private WebHook WebHook;
        private GameManager GameManager;
        public List<Player> GameWinner { get; set; }
        public List<Card> PlayerCards { get; set; }
        public List<Card> CardsOnBoard { get; set; }
        public int HandValue { get; set; }

        public CardManager(List<Card> playerCards, List<Card> cardsOnBoard)
        {
            PlayerCards = playerCards;
            CardsOnBoard = cardsOnBoard;
        }

        public void ValidatePlayerHand(List<Card> playerHand, List<Card> cardsOnBoard)
        {
            List<Card> cardsToCheck = new List<Card>();
            cardsToCheck.AddRange(playerHand);
            cardsToCheck.AddRange(cardsOnBoard);
        }

        public void ValidateCardsInHand(List<Card> cards) 
        {
            
        }

        public void ValidateCardsOnBoard(List<Card> cards)
        {

        }
    }
}
