using GameEngine.Core.Enums;
using GameEngine.Models.Game;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameEngine.Core.Managers
{
    public class CardManager : ICardManager
    {
        public List<Player> Players { get; set; }
        public List<Player> GameWinners { get; set; }
        public PokerTable Table { get; set; }
        public PokerHand? HandValue { get; set; }
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
        public CardManager(List<Player> players, PokerTable table)
        {
            Players = players;
            Table = table;
        }

        public List<Player> GetPlayerHandValue(List<Card> playerCards, List<Card> boardCards)
        {
            List<Player> players = new List<Player>();
            players.Add(new Player()
            {
                Id = 1,
                Cards = playerCards,
            });
            players.Add(new Player()
            {
                Id = 2,
                Cards = playerCards,
            });

            for (int i = 0; i < players.Count; i++)
            {
                List<Card> cards = new List<Card>();
                cards.AddRange(playerCards);
                cards.AddRange(boardCards);
                CheckForHighestCard(cards);
                GetHandValue(cards);

            }
            return GameWinners;
        }

        private async void GetHandValue(List<Card> cards)
        {
            var tasks = new List<Task<bool>>();
            tasks.Add(CheckForRoyalFlush(cards));
            tasks.Add(CheckForStraightFlush(cards));
            tasks.Add(CheckForFourOfAKind(cards));
            tasks.Add(CheckForFullHouse(cards));
            tasks.Add(CheckForFlush(cards));
            tasks.Add(CheckForStraight(cards));
            tasks.Add(CheckForThreeOfAKind(cards));
            tasks.Add(CheckForTwoPairs(cards));
            tasks.Add(CheckForOnePair(cards));
            await Task.WhenAny(tasks);
        }

        private async Task<bool> CheckForRoyalFlush(List<Card> cardsToCheck)
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

        private async Task<bool> CheckForStraightFlush(List<Card> cards)
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


        private async void CheckForHighestCard(List<Card> cards)
        {
            HighestCard = cards.OrderBy(x => x.Type).ElementAt(cards.Count - 1);
        }

        private async Task<bool> CheckForStraight(List<Card> cards)
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
                        sequences.Add(new Card()
                        {
                            Type = NextCardValue,
                            Symbol = NextCardSymbol,
                        });
                    }

                    else
                    {
                        sequences.Add(new Card()
                        {
                            Type = CurrentCardValue,
                            Symbol = CurrentCardSymbol
                        });
                    }
                }
            }
            SequenceCounter = 1;
            return sequences;
        }

        private async Task<bool> CheckForFullHouse(List<Card> cards)
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

        private async Task<bool> CheckForFourOfAKind(List<Card> cards)
        {
            IEnumerable<Card> fourOfAKind = cards.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 4);
            if (fourOfAKind != null)
            {
                HandValue = PokerHand.FourOfAKind;
                return true;
            }
            return false;
        }

        private async Task<bool> CheckForThreeOfAKind(List<Card> cardsToCheck)
        {
            IEnumerable<Card> threeOfAKind = cardsToCheck.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 3);
            if (threeOfAKind != null)
            {
                HandValue = PokerHand.ThreeOfAKind;
                return true;
            }
            return false;
        }

        private async Task<bool> CheckForTwoPairs(List<Card> cardsToCheck)
        {
            var twoPairsQuery = cardsToCheck.GroupBy(x => x.Symbol).Where(y => y.Count() == 2);
            if (twoPairsQuery != null)
            {
                HandValue = PokerHand.TwoPair;
                return true;
            }
            return false;
        }

        private async Task<bool> CheckForOnePair(List<Card> cardsToCheck)
        {
            IEnumerable<Card> onePair = cardsToCheck.GroupBy(x => x.Type).FirstOrDefault(x => x.Count() == 2);
            if (onePair != null)
            {
                HandValue = PokerHand.OnePair;
                return true;
            }
            return false;
        }
    }
}
