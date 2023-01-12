using GameEngine.Core.Enums;
using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class CardManager : ICardManager
    {
        private WebHook WebHook;
        private GameManager GameManager;
        public List<Player> GameWinner { get; set; }
        public List<Card> Cards { get; set; }
        public PokerHand HandValue { get; set; }

        public Card HighestCard { get; set; }

        private int DuplicateCounter = 1;
        private int SequenceCounter = 1;

        private CardTypes CurrentCardValue;
        private CardTypes NextCardValue;

        private Symbols CurrentCardSymbol;
        private Symbols NextCardSymbol;

        public CardManager()
        {

        }
        public CardManager(List<Card> cards)
        {
            Cards = cards;
        }

        public void GetPlayerHandValue(List<Card> playerCards, List<Card> boardCards)
        {
            List<Card> sortedCards = new List<Card>();
            sortedCards.AddRange(playerCards);
            sortedCards.AddRange(boardCards);

            //Sorts the incoming cards in card values for checking
            sortedCards = sortedCards.OrderBy(x => x.Type).ToList();

            //Dictionary that holds duplicate cards
            List<Card> duplicates = new List<Card>();
            //IDictionary<CardTypes, int> duplicates = new Dictionary<CardTypes, int>();

            //List of sequences
            List<Card> sequences = new List<Card>();

            //Checks for the highest card
            HighestCard = sortedCards.ElementAt(sortedCards.Count - 1);

            //Looks at current card, next card, and looks for duplicate cards or sequences of cards
            for (int i = 0; i < sortedCards.Count; i++)
            {


                if (i != sortedCards.Count - 1)
                {
                    CurrentCardValue = sortedCards[i].Type;
                    CurrentCardSymbol = sortedCards[i].Symbol;

                    NextCardValue = sortedCards[i + 1].Type;
                    NextCardSymbol = sortedCards[i + 1].Symbol;
                }

                //Checks for duplicate card values
                if (CurrentCardValue == NextCardValue)
                {
                    DuplicateCounter++;
                    duplicates.Add(new Card(CurrentCardValue, CurrentCardSymbol));
                    DuplicateCounter = 1;
                }

                //Checks for a card sequence, example: 7,8 or 4,5
                if (NextCardValue == CurrentCardValue + 1)
                {

                    if (SequenceCounter <= 2)
                    {
                        sequences.Add(new Card(NextCardValue, NextCardSymbol));
                    }

                    if (i == sortedCards.Count - 1)
                    {
                        sequences.Add(new Card(NextCardValue, NextCardSymbol));
                    }

                    else
                    {
                        SequenceCounter++;
                        sequences.Add(new Card(CurrentCardValue, CurrentCardSymbol));
                    }
                }

            }
            IsRoyalFlush(sequences);
        }

        private bool IsRoyalFlush(List<Card> cardsToCheck)
        {
            bool isValuesCorrect = false;
            bool isSymbolsCorrect = false;
            cardsToCheck.OrderByDescending(x => x.Type);
            if (cardsToCheck[0].Type == CardTypes.Ace
                && cardsToCheck[1].Type == CardTypes.King
                && cardsToCheck[2].Type == CardTypes.Queen
                && cardsToCheck[3].Type == CardTypes.Pawn
                && cardsToCheck[4].Type == CardTypes.Ten)
                isValuesCorrect = true;

            if (cardsToCheck[0].Symbol == cardsToCheck[4].Symbol
                && cardsToCheck[1].Symbol == cardsToCheck[4].Symbol
                && cardsToCheck[2].Symbol == cardsToCheck[4].Symbol
                && cardsToCheck[3].Symbol == cardsToCheck[4].Symbol)
                isSymbolsCorrect = true;

            if (isValuesCorrect && isSymbolsCorrect)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void IsStraightFlush()
        {

        }
    }
}
