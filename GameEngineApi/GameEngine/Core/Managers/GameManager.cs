using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class GameManager
    {
        private readonly WebHook _webHook;

        public GameManager(WebHook webHook)
        {
            _webHook = webHook;
        }

        // public void GetCurrentGame(GameState gameState)
        // {
        //     // User GameStateService to get the current game state
        //     // this should get called when the gameController get call 
        //     // Update this
        //     // Game stat is used to see the previous players turn and set the next players turn
        // } 
        //
        // public GameState UpdateGameState()
        // {
        //     GameState gameState; // get from service 
        //     // used to update the whole game.
        // }
        //
        // public void SetPlayerTurn()
        // {
        //
        // }
        //
        // public GameState UpdateChipsPool()
        // {
        //
        // }
        //
        // public GameState ResetPlayerBet()
        // {
        //
        // }
        //
        // public GameState GiveCards(int amountOfCards)
        // {
        //
        // }
        //
        // public GameState ClearTable()
        // {
        //
        // }
        //
        // public GameState GetNewCardDeck(GameState gameState)
        // {
        //
        // }
        //
        // public void EndGame(GameState gameState)
        // {
        //     GivePlayerStats(new Player());
        // }
        //
        // private void GivePlayerStats(Player player)
        // {
        //
        // }
    }
}
