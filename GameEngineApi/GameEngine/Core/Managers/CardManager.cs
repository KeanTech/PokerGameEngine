using GameEngine.Core.Enums;
using GameEngine.Models.Game;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameEngine.Core.Managers
{
    public class CardManager : ICardManager
    {
        public List<Player> Players { get; set; }
        public List<Player> GameWinners { get; set; }
        public Table Table { get; set; }
        public PokerHand? HandValue { get; set; }
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
        public CardManager(List<Player> players, Table table)
        {
            Players = players;
            Table = table;
        }

        public List<Player> GetPlayerHandValue(List<Card> playerCards, List<Card> boardCards)
        {


            for (int i = 0; i < Players.Count; i++)
            {
                List<Card> cards = new List<Card>();
                cards.AddRange(playerCards);
                cards.AddRange(boardCards);
                //cards.AddRange(Players[i].Cards);
                //cards.AddRange(Table.Cards);
                CheckForRoyalFlush(cards);
                CheckForStraightFlush(cards);
                CheckForFourOfAKind(cards);
                CheckForFullHouse(cards);
                CheckForFlush(cards);
                CheckForStraight(cards);
                CheckForThreeOfAKind(cards);
                CheckForTwoPairs(cards);
                CheckForOnePair(cards);
                CheckForHighestCard(cards);
            }
            return GameWinners;
        }

        //private async PokerHand GetHandValue(List<Card> cards)
        //{
        //    var tasks = new List<Task<bool>>();
        //    tasks.Add(CheckForFlush(cards));
        //    await Task.WhenAny(tasks);
        //}

        private bool CheckForRoyalFlush(List<Card> cardsToCheck)
        {
            List<Card> sequencedCards = CheckForSequences(cardsToCheck);
            IEnumerable<Card> isFlush = sequencedCards.GroupBy(x => x.Symbol).Where(x => x.Count() >= 5).FirstOrDefault();
            if (isFlush.Any(x => x.Type == CardTypes.Ace))
            {
                HandValue = PokerHand.RoyalFlush;
                return true;
            }
            return false;
        }

        private bool CheckForStraightFlush(List<Card> cards)
        {
            List<Card> sequencedCards = CheckForSequences(cards);
            IEnumerable<Card> straightFlush = sequencedCards.GroupBy(x => x.Symbol).Where(y => y.Count() >= 5).FirstOrDefault();
            if (straightFlush != null)
            {
                HandValue = PokerHand.StraightFlush;
                return true;
            }
            return false;
        }

        private async Task<bool> CheckForFlush(List<Card> cardsToCheck)
        {
            List<Card> sequencedCards = CheckForSequences(cardsToCheck);
            IEnumerable<Card> flush = sequencedCards.GroupBy(x => x.Symbol).Where(x => x.Count() == 5).SelectMany(x => x.OrderBy(x => x.Symbol)).Take(5);
            if (flush != null)
            {
                HandValue = PokerHand.Flush;
                return true;
            }
            return false;
        }


        private void CheckForHighestCard(List<Card> cards)
        {
            HighestCard = cards.OrderBy(x => x.Type).ElementAt(cards.Count - 1);
        }

        private bool CheckForStraight(List<Card> cards)
        {
            List<Card> sequencesCards = CheckForSequences(cards);
            IEnumerable<Card> noDuplicates = sequencesCards.Distinct().ToList();
            IEnumerable<Card> straight = noDuplicates.OrderByDescending(x => x.Type).Take(5);
            if (straight != null)
            {
                HandValue = PokerHand.Straight;
                return true;
            }
            return false;
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
            return false;
        }

        private bool CheckForFourOfAKind(List<Card> cards)
        {
            IEnumerable<Card> fourOfAKind = cards.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 4);
            if (fourOfAKind != null)
            {
                HandValue = PokerHand.FourOfAKind;
                return true;
            }
            return false;
        }

        private bool CheckForThreeOfAKind(List<Card> cardsToCheck)
        {
            IEnumerable<Card> threeOfAKind = cardsToCheck.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 3);
            if (threeOfAKind != null)
            {
                HandValue = PokerHand.ThreeOfAKind;
                return true;
            }
            return false;
        }

        private bool CheckForTwoPairs(List<Card> cardsToCheck)
        {
            var twoPairsQuery = cardsToCheck.GroupBy(x => x.Symbol).Where(y => y.Count() == 2);
            if (twoPairsQuery != null)
            {
                HandValue = PokerHand.TwoPair;
                return true;
            }
            return false;
        }

        private bool CheckForOnePair(List<Card> cardsToCheck)
        {
            IEnumerable<Card> onePair = cardsToCheck.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 2);
            if (onePair != null)
            {
                HandValue = PokerHand.OnePair;
                return true;
            }
            return false;
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
