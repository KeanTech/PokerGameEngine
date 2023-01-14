using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class DataManager
    {
        public static IList<DeckCard> GetDeckCards(IList<Card> cards, PokerTable pokerTable)
        {
            IList<DeckCard> deckCards = new List<DeckCard>();

            foreach (var card in cards)
            {
                DeckCard deckCard = new DeckCard() 
                { 
                    Card = card,
                    CardId = card.Id,
                    PokerTable = pokerTable,
                    TableId = pokerTable.Id
                };
                
                deckCards.Add(deckCard);
            }

            return deckCards;
        }

        public static IList<Card> GetCardsFromDeck(IList<DeckCard> deckCards) 
        {
            IList<Card> cards = new List<Card>();  

            foreach (var deckCard in deckCards)
            {
                cards.Add(deckCard.Card);
            }

            return cards;   
        }

    }
}
