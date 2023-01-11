using GameEngine.Core.Enums;
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

        public GameManager(WebHook webHook)
        {
            _webHook = webHook;
        }

        public GameState StartNewGame(int tabelId) 
        {
            // Get table from service/db with table id

            GameState gameState = new GameState() 
            {
                PokerTable = new Table() {  }
            };

            return gameState;   
        }

        public void GetCurrentGame(GameState gameState)
        {
            // User GameStateService to get the current game state
            // this should get called when the gameController get call 
            // Update this
            // Game stat is used to see the previous players turn and set the next players turn
        } 

        public void UpdateGameState(GameState gameState)
        {
            // Update the gamestate in the database
        }

        public void SetPlayerTurn()
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

        public GameState GiveCards(int amountOfCards)
        {
            
        }

        public GameState ClearTable()
        {
            return new GameState();
        }

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
