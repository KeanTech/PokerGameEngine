using GameEngine.Core.Enums;
using GameEngine.Models.Events;
using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class GameManager : IGameManager
    {
        private readonly WebHook _webHook;

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

        //public GameManager(WebHook webHook)
        //{
        //    _webHook = webHook;
        //}

        public GameState StartNewGame(int tabelId) 
        {
            // Get table from service/db with table id

            //GameState gameState = new GameState()
            //{
            //    PokerTable = new PokerTable() 
            //    { 
            //        Players = new List<Player>() 
            //        { 
            //            new Player() { Cards = new List<Card>(), Id = 1, Accessories = new List<Accessory>(), ChipValue = 200, Name = "", UserIdentifier = "" },
            //            new Player() { Cards = new List<Card>(), Id = 2, Accessories = new List<Accessory>(), ChipValue = 100, Name = "", UserIdentifier = "" }
            //        },
            //    CardDeck = _cards, Cards = new Stack<Card>(), ChipsValue = 10  },
            //    CurrentPlayerId = 1,
            //    PlayerIdentifier = "Alwjdpawdw1oajdp034"
            //};

            GameState gameState = new GameState();

            return gameState;   
        }

        private int GetHighestBet(List<Player> players) 
        {
            int highestBet = players.Max(x => x.CurrentBet);

            return highestBet;
        }
        private Player? FindNextPlayer(GameState gameState)
        {
            Player? currentPlayer = gameState.PokerTable.Players.FirstOrDefault(x => x.Id == gameState.CurrentPlayerId);

            if (currentPlayer == null)
                return null;

            int playerIndex = gameState.PokerTable.Players.IndexOf(currentPlayer);
            // make check for fold

            if (playerIndex == gameState.PokerTable.Players.Count - 1)
                return gameState.PokerTable.Players.First();

            return gameState.PokerTable.Players[playerIndex + 1];
        }

        public GameState GetCurrentGame(int tableId)
        {
            // User GameStateService to get the current game state
            // this should get called when the gameController get call 
            // Update this
            // Game stat is used to see the previous players turn and set the next players turn

            return new GameState();
        } 

        public GameState UpdateGameState(GameState gameState)
        {
            // Update the gamestate in the database

            return gameState;
        }

        public GameState UpdateChipsPool(GameState gameState, int updateValue)
        {
            gameState.PokerTable.ChipsValue += updateValue;
            UpdateGameState(gameState);
            
            return gameState;
        }

        public GameState RemovePlayerCards(GameState gameState)
        {
            foreach (var player in gameState.PokerTable.Players)
            {
                player.Cards.Clear();
            }

            UpdateGameState(gameState);

            return gameState;
        }
        public GameState ResetPlayerBets(GameState gameState)
        {
            foreach (var player in gameState.PokerTable.Players)
            {
                player.CurrentBet = 0;
            }

            UpdateGameState(gameState);

            return gameState;
        }

        public GameState GiveCardsToTable(int amountOfCards, GameState gameState) 
        {
            return new GameState();
        }

        public GameState GiveCardsToPlayers(GameState gameState)
        {
            if (gameState.PokerTable != null)
            {
                Stack<Card> cards;

                if (gameState.PokerTable.CardDeck.Count > 0)
                    cards = new Stack<Card>();
                for (int i = 0; i < 2; i++)
                {
                    foreach (var player in gameState.PokerTable.Players)
                    {
                        player.Cards.Add();
                    }
                }

                UpdateGameState(gameState);
            }

            return gameState;
        }

        public GameState ClearTable(GameState gameState)
        {
            gameState.PokerTable.Cards.Clear();
            gameState.PokerTable.CardDeck.Clear();
            gameState.PokerTable.ChipsValue = 0;

            RemovePlayerCards(gameState);
            ResetPlayerBets(gameState);

            UpdateGameState(gameState);

            return gameState;
        }
        
        public GameState GetNewCardDeck(GameState gameState)
        {
            List<Card> cards = new List<Card>();
            _cards = new Stack<Card>(cards);
            gameState.PokerTable.Cards = _cards;

            return new GameState();
        }

        public void EndGame(GameState gameState)
        {
            GivePlayerStats(new Player());
        }

        private void GivePlayerStats(User user)
        {
            user.Wins++;
            user.ChipsAquired += player.ChipValue;

            // save player(User)
        }

        public void SetPlayerTurn(GameState gameState)
        {
            Player? nextPlayer = FindNextPlayer(gameState);
            
            if (nextPlayer == null)
                return;

            gameState.CurrentPlayerId = nextPlayer.Id;
            gameState.PlayerIdentifier = nextPlayer.UserIdentifier;
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
                player.ChipValue -= betEvent.BetAmount;

                gameState.PokerTable.ChipsValue += betEvent.BetAmount;

                SetPlayerTurn(gameState);

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
