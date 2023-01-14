using GameEngine.Core.Enums;
using GameEngine.Data;
using GameEngine.Models.Events;
using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class GameManager : IGameManager
    {
        private static Card[] defaultCardDeck = new Card[] 
        {   
            new Card() 
            {
                Id = 0, Symbol = Symbols.Club, Type = CardTypes.Two 
            },
            new Card()
            {
                Id = 1, Symbol = Symbols.Club, Type = CardTypes.Three
            }, 
            new Card()
            {
                Id = 2, Symbol = Symbols.Club, Type = CardTypes.Four
            },
            new Card()
            {
                Id = 3, Symbol= Symbols.Club, Type = CardTypes.Five
            },
            new Card()
            {
                Id = 4, Symbol = Symbols.Club, Type = CardTypes.Six
            }
        };
        private Stack<Card> _cards = new Stack<Card>(defaultCardDeck);
        private readonly GameEngineContext _context;

        public GameManager(GameEngineContext context) 
        {
            _context = context;
        }

        public async Task<PokerTable> StartNewGame(PokerTable pokerTable) 
        {
            _context.Table.Update(pokerTable);
            await _context.SaveChangesAsync();

            return pokerTable;   
        }

        private int GetHighestBet(IList<Player> players) 
        {
            int highestBet = players.Max(x => x.CurrentBet);

            return highestBet;
        }
        private Player? FindNextPlayer(PokerTable pokerTable)
        {
            Player? currentPlayer = pokerTable.Players.FirstOrDefault(x => x.Id == pokerTable.CurrentPlayerId);

            if (currentPlayer == null)
                return null;

            int playerIndex = pokerTable.Players.IndexOf(currentPlayer);
            // make check for fold

            if (playerIndex == pokerTable.Players.Count - 1)
                return pokerTable.Players.First();

            return pokerTable.Players[playerIndex + 1];
        }

        public PokerTable GetCurrentGame(int tableId)
        {
            // User GameStateService to get the current game state
            // this should get called when the gameController get call 
            // Update this
            // Game stat is used to see the previous players turn and set the next players turn

            return new GameState();
        } 

        public async Task<PokerTable> UpdateGameState(PokerTable pokerTable)
        {
            if (pokerTable != null)
            { 
                _context.Table.Update(pokerTable);
                await _context.SaveChangesAsync();
                return pokerTable;
            }    

            return new PokerTable();
        }

        public async Task<PokerTable> UpdateChipsPool(PokerTable pokerTable, int updateValue)
        {
            pokerTable.ChipsValue += updateValue;
            await UpdateGameState(pokerTable);
            
            return pokerTable;
        }

        public async Task<PokerTable> RemovePlayerCards(PokerTable pokerTable)
        {
            foreach (var player in pokerTable.Players)
            {
                player.Cards.Clear();
            }

            await UpdateGameState(pokerTable);

            return pokerTable;
        }
        public async Task<PokerTable> ResetPlayerBets(PokerTable pokerTable)
        {
            foreach (var player in pokerTable.Players)
            {
                player.CurrentBet = 0;
            }

            await UpdateGameState(pokerTable);

            return pokerTable;
        }

        public async Task<PokerTable> GiveCardsToTable(int amountOfCards, PokerTable pokerTable) 
        {
            if (pokerTable.CardDeck != null && pokerTable.CardDeck.Count > 0)
            {
                Stack<Card> cards = new Stack<Card>(DataManager.GetCardsFromDeck(pokerTable.CardDeck));
                
                for (int i = 0; i < amountOfCards; i++)
                {
                    pokerTable.Cards.Add(new TableCard() { TableId = pokerTable.Id, Card = cards.Pop() });
                }

                pokerTable.CardDeck = DataManager.GetDeckCards(cards.ToList(), pokerTable);

                await UpdateGameState(pokerTable);
            }

            return pokerTable;
        }

        public async Task<PokerTable> GiveCardsToPlayers(PokerTable pokerTable)
        {
            if (pokerTable.CardDeck != null && pokerTable.CardDeck.Count > 0)
            {
                Stack<Card> cards = new Stack<Card>(DataManager.GetCardsFromDeck(pokerTable.CardDeck));

                for (int i = 0; i < 2; i++)
                {
                    foreach (var player in pokerTable.Players)
                    {
                        int cardId = cards.Peek().Id;   
                        player.Cards.Add(new PlayerCard() { PlayerId = player.Id, Card = cards.Pop(), CardId = cardId });
                    }
                }
                pokerTable.CardDeck = DataManager.GetDeckCards(cards.ToList(), pokerTable);

                await UpdateGameState(pokerTable);
            }

            return pokerTable;
        }

        public async Task<PokerTable> ClearTable(PokerTable pokerTable)
        {
            pokerTable.Cards?.Clear();
            pokerTable.CardDeck?.Clear();
            pokerTable.ChipsValue = 0;

            await RemovePlayerCards(pokerTable);
            await ResetPlayerBets(pokerTable);
            await UpdateGameState(pokerTable);

            return pokerTable;
        }
        
        public PokerTable GetNewCardDeck(PokerTable pokerTable)
        {

            return pokerTable;
        }

        public void EndGame(PokerTable pokerTable)
        {
            GivePlayerStats(new User());
        }

        private void GivePlayerStats(User user)
        {
            user.Wins++;
            user.ChipsAquired += user.Player.ChipsValue;

            // save player(User)
        }

        /// <summary>
        /// Made for test reasons
        /// </summary>
        /// <param name="betEvent"></param>
        /// <param name="betEventType"></param>
        /// <returns></returns>
        public bool PlayerBet(BetEvent betEvent, string betType) 
        {
            GameState gameState = new GameState();
            Player? player = gameState.PokerTable.Players.FirstOrDefault(x => x.Id == betEvent.PlayerId);

            if (player != null)
            {
                if (player.ChipsValue < betEvent.BetAmount)
                    return false;

                player.CurrentBet = betEvent.BetAmount;
                player.ChipsValue -= betEvent.BetAmount;

                gameState.PokerTable.ChipsValue += betEvent.BetAmount;
                
                return true;
            }

            return false;
        }

        public bool PlayerTurnEvent(TurnEvent turnEvent, string turnType) 
        {
            GameState gameState = new GameState();
            Player? player = gameState.PokerTable.Players.FirstOrDefault(x => x.Id == turnEvent.PlayerId);
            int highestBet = GetHighestBet(gameState.PokerTable.Players);
            
            if (player != null)
            {
                switch (turnType)
                {
                    case "Fold":
                        player.IsFolded = true; 
                        SetPlayerTurn(gameState);
                        return true;

                    case "Check":
                        if (player.CurrentBet == highestBet)
                        { 
                            SetPlayerTurn(gameState);
                            return true;
                        }

                        return false;
                    
                    default:
                        return false;
                }
            }

            return false;
        }

        public bool PlayerCall(BetEvent betEvent)
        {
            return PlayerBet(betEvent, "Call");
        }

        public bool PlayerRaise(BetEvent betEvent)
        {
            return PlayerBet(betEvent, "Raise");
        }

        public bool PlayerAllIn(BetEvent betEvent)
        {
            return PlayerBet(betEvent, "AllIn");
        }

        public bool PlayerFold(TurnEvent turnEvent)
        {
            return PlayerTurnEvent(turnEvent, "Fold");
        }

        public bool PlayerCheck(TurnEvent turnEvent)
        {
            return PlayerTurnEvent(turnEvent, "Check");
        }
    }
}
