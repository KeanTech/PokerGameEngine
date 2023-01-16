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
        public List<Card> PlayerHand { get; set; }
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
            List<Card> cards = new List<Card>();
            cards.AddRange(playerCards);
            cards.AddRange(boardCards);

            IsRoyalFlush(cards);
            //IsStraightFlush(cards);
            //IsFlush(cards);
            //IsStraight(cards);
            //CheckForFullHouse(cards);
            //CheckForThreeOfAKind(cards);
            //CheckForTwoPairs(cards);
            //CheckForOnePair(cards);
            //CheckForHighestCard(cards);
            //CheckForDuplicates(cards);
            //CheckForSequences(cards);
            //Looks at current card, next card, and looks for duplicate cards or sequences of cards
            //IsRoyalFlush(cards);
            //IsStraightFlush(sequences);

        }

        private bool IsRoyalFlush(List<Card> cardsToCheck)
        {
            List<Card> sequencedCards = CheckForSequences(cardsToCheck);
            IEnumerable<Card> isFlush = sequencedCards.GroupBy(x => x.Symbol).Where(x => x.Count() >= 5).FirstOrDefault();
            if (isFlush.Any(x => x.Type == CardTypes.Ace))
            {
                HandValue = PokerHand.RoyalFlush;
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsStraightFlush(List<Card> cards)
        {
            List<Card> sequencedCards = CheckForSequences(cards);
            IEnumerable<Card> straightFlush = sequencedCards.GroupBy(x => x.Symbol).Where(y => y.Count() >= 5).FirstOrDefault();
            if (straightFlush != null)
            {
                HandValue = PokerHand.StraightFlush;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsFlush(List<Card> cardsToCheck)
        {
            List<Card> sequencedCards = CheckForSequences(cardsToCheck);
            IEnumerable<Card> flush = sequencedCards.GroupBy(x => x.Symbol).Where(x => x.Count() == 5).SelectMany(x => x.OrderBy(x => x.Symbol)).Take(5);
            if (flush != null)
            {
                HandValue = PokerHand.Flush;
                return true;
            }
            else
            {
                return false;
            }
        }


        private void CheckForHighestCard(List<Card> cards)
        {
            HighestCard = cards.OrderBy(x => x.Type).ElementAt(cards.Count - 1);
        }

        private bool IsStraight(List<Card> cards)
        {
            List<Card> sequencesCards = CheckForSequences(cards);
            IEnumerable<Card> noDuplicates = sequencesCards.Distinct().ToList();
            IEnumerable<Card> straight = noDuplicates.OrderByDescending(x => x.Type).Take(5);
            if (straight != null)
            {
                HandValue = PokerHand.Straight;
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Card> CheckForSequences(List<Card> cards)
        {
            List<Card> sequences = new List<Card>();
            cards = cards.OrderBy(x => x.Type).ToList();

            for (int i = 0; i < cards.Count; i++)
            {
                if (i != cards.Count - 1)
                {
                    CurrentCardValue = cards[i].Type;
                    CurrentCardSymbol = cards[i].Symbol;

                    NextCardValue = cards[i + 1].Type;
                    NextCardSymbol = cards[i + 1].Symbol;
                }

                //Checks for a card sequence, example: 7, 8 or 4, 5
                if (NextCardValue == CurrentCardValue + 1)
                {
                    SequenceCounter++;
                    //If its a start of a sequence

                    //If the sequence ends on the last cards in the list
                    if (i == cards.Count - 1)
                    {
                            sequences.Add(new Card(NextCardValue, NextCardSymbol));
                    }

                    else
                    {
                        sequences.Add(new Card(CurrentCardValue, CurrentCardSymbol));
                    }
                }
            }
            return sequences;
        }

        private bool CheckForFullHouse(List<Card> cards)
        {
            IEnumerable<Card> threeOfAKind = cards.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 3);
            if (threeOfAKind != null)
            {
                var remainingCards = cards.Where(x => !threeOfAKind.Any(y => x.Type == y.Type));
                IEnumerable<Card> pair = remainingCards.GroupBy(x => x.Type).Where(x => x.Count() >= 2).OrderByDescending(y => y.Key).FirstOrDefault();
                if (pair != null)
                {
                    HandValue = PokerHand.FullHouse;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CheckForFourOfAKind(List<Card> cards)
        {
            IEnumerable<Card> fourOfAKind = cards.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 4);
            if (fourOfAKind != null)
            {
                HandValue = PokerHand.FourOfAKind;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckForThreeOfAKind(List<Card> cardsToCheck)
        {
            IEnumerable<Card> threeOfAKind = cardsToCheck.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 3);
            if (threeOfAKind != null)
            {
                HandValue = PokerHand.ThreeOfAKind;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckForTwoPairs(List<Card> cardsToCheck)
        {
            var twoPairsQuery = cardsToCheck.GroupBy(x => x.Symbol).Where(y => y.Count() == 2);
            if (twoPairsQuery != null)
            {
                HandValue = PokerHand.TwoPair;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckForOnePair(List<Card> cardsToCheck)
        {
            IEnumerable<Card> onePair = cardsToCheck.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 2);
            if (onePair != null)
            {
                HandValue = PokerHand.OnePair;
                return true;
            }
            else
            {
                return false;
            }
        }

        //private List<Card> CheckForSequences(List<Card> cards)
        //{
        //    List<Card> sequences = new List<Card>();
        //    cards = cards.OrderBy(x => x.Type).ToList();

        //    //List<Card> testOne = cards.GroupBy(x => (int)x.Symbol).ToList();

        //    for (int i = 0; i < cards.Count; i++)
        //    {
        //        if (i != cards.Count - 1)
        //        {
        //            CurrentCardValue = cards[i].Type;
        //            CurrentCardSymbol = cards[i].Symbol;

        //            NextCardValue = cards[i + 1].Type;
        //            NextCardSymbol = cards[i + 1].Symbol;
        //        }

        //        //Checks for a card sequence, example: 7, 8 or 4, 5
        //        if (NextCardValue == CurrentCardValue + 1)
        //            {
        //                SequenceCounter++;
        //                //If its a start of a sequence
        //            if (SequenceCounter <= 2)
        //                {

        //                    sequences.Add(new Card(CurrentCardValue, CurrentCardSymbol));
        //                    sequences.Add(new Card(NextCardValue, NextCardSymbol));
        //                }

        //                //If the sequence ends on the last cards in the list
        //            else if (i == cards.Count - 1)
        //                {
        //                    sequences.Add(new Card(NextCardValue, NextCardSymbol));
        //                }

        //                else
        //                {
        //                    sequences.Add(new Card(CurrentCardValue, CurrentCardSymbol));
        //                }
        //            }
        //    }
        //    return sequences;
        //}


        //private List<Card> CheckForDuplicates(List<Card> cards)
        //{
        //    cards = cards.OrderBy(x => x.Type).ToList();
        //    List<Card> duplicates = new List<Card>();

        //    for (int i = 0; i < cards.Count; i++)
        //    {


        //        if (i != cards.Count - 1)
        //        {
        //            CurrentCardValue = cards[i].Type;
        //            CurrentCardSymbol = cards[i].Symbol;

        //            NextCardValue = cards[i + 1].Type;
        //            NextCardSymbol = cards[i + 1].Symbol;
        //        }

        //        //Checks for duplicate card values
        //        if (CurrentCardValue == NextCardValue)
        //        {
        //            DuplicateCounter++;
        //            duplicates.Add(new Card(CurrentCardValue, CurrentCardSymbol));
        //            //DuplicateCounter = 1;
        //        }
        //    }
        //    return duplicates;
        //}
    }
}
