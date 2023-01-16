using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public static class DataManager
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

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
