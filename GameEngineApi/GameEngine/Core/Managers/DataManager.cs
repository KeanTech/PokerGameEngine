using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class DataManager
    {
        public static List<DeckCard> GetDeckCards(List<Card> cards, GameState gameState)
        {
            List<DeckCard> deckCards = new List<DeckCard>();

            foreach (var card in cards)
            {
                DeckCard deckCard = new DeckCard() 
                { 
                    Card = card,
                    CardId = card.Id,
                    PokerTable = gameState.PokerTable,
                    TableId = gameState.PokerTable.Id
                };
                
                deckCards.Add(deckCard);
            }

            return deckCards;
        }

        public static List<Card> GetCardsFromDeck(List<DeckCard> deckCards) 
        {
            List<Card> cards = new List<Card>();  

            foreach (var deckCard in deckCards)
            {
                cards.Add(deckCard.Card);
            }

            return cards;   
        }

    }
}
