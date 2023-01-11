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

        public void SetPlayerTurn()
        {

        }

        public void PlayerCall(BetEvent callEvent) 
        {
            GameState gameState = GetCurrentGame(callEvent.PokerTableId);


        }

        public void PlayerRaise() 
        {
            
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
    }
}
