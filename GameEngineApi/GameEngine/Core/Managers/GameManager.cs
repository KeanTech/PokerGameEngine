using GameEngine.Core.Enums;
using GameEngine.Models.Events;
using GameEngine.Models.Game;
using Microsoft.AspNetCore.Razor.Language.Extensions;

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

            GameState gameState = new GameState()
            {
                PokerTable = new Table() 
                { 
                    Players = new List<Player>() 
                    { 
                        new Player() { Cards = new List<Card>(), Id = 1, Accessories = new List<Accessory>(), Chips = new Dictionary<int, int>(), Name = 12, UserIdentifier = "" },
                        new Player() { Cards = new List<Card>(), Id = 2, Accessories = new List<Accessory>(), Chips = new Dictionary<int, int>(), Name = 15, UserIdentifier = "" }
                    },
                CardDeck = _cards.ToList(), Cards = new List<Card>(), Chips = new Dictionary<int, int>()  },
                CurrentPlayerId = 1,
                PlayerIdentifier = "Alwjdpawdw1oajdp034"
            };

            return gameState;   
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

        public void UpdateGameState(GameState gameState)
        {
            // Update the gamestate in the database
        }

        public GameState UpdateChipsPool()
        {
            return new GameState();
        }

        public GameState ResetPlayerBet()
        {
            return new GameState();
        }

        /// <summary>
        /// This method will update the gameState so that players have cards in their card List 
        /// </summary>
        /// <param name="amountOfCards"></param>
        /// <param name="gameState"></param>
        /// <returns></returns>
        public GameState GiveCards(int amountOfCards, GameState gameState)
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var player in gameState.PokerTable.Players)
                {
                    player.Cards.Add(new Card() { Symbol = Symbols.Club, Type = CardTypes.Two});
                }
            }

            return gameState;
        }

        /// <summary>
        /// This method clears the Table pool, playercards, playerBets, and tableCards
        /// </summary>
        /// <returns></returns>
        public GameState ClearTable()
        {
            return new GameState();
        }

        /// <summary>
        /// This method will generate a new deck for the table
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns></returns>
        public GameState GetNewCardDeck(GameState gameState)
        {
            return new GameState();
        }

        public void EndGame(GameState gameState)
        {
            GivePlayerStats(new Player());
        }

        private void GivePlayerStats(Player player)
        {

        }

        public void SetPlayerTurn(GameState gameState)
        {
            Player? nextPlayer = FindNextPlayer(gameState);
            
            if (nextPlayer == null)
                return;

            gameState.CurrentPlayerId = nextPlayer.Id;
            gameState.PlayerIdentifier = nextPlayer.UserIdentifier;
        }

        public void PlayerCall(BetEvent betEvent)
        {
            GameState gameState = new GameState();
            Player? player = gameState.PokerTable.Players.FirstOrDefault(x => x.Id == betEvent.PlayerId);

            if (player != null)
            {
                player.CurrentBet = betEvent.BetAmount;
                SetPlayerTurn(gameState);
                
                // Remove betAmount from chips value
                // Add bet to table
            }
        }

        public void PlayerRaise(BetEvent betEvent)
        {
            throw new NotImplementedException();
        }

        public void PlayerAllIn(BetEvent betEvent)
        {
            throw new NotImplementedException();
        }

        public void PlayerFold(int playerId, string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public void PlayerCheck(int playerId, string userIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
