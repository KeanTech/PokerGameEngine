namespace GameEngine.Core.Managers
{
    public class GameManager
    {
        private readonly WebHook _webHook;
        private readonly string _gameState;

        public GameManager(WebHook webHook, string gameState) 
        {
            _webHook = webHook;
            _gameState = gameState;
        }

        private void GetCurrentGame(GameState gameState) 
        {
            // User GameStateService to get the current game state
            // this should get called when the gameController get call 

            // Game stat is used to see the previous players turn and set the next players turn
        }




    }
}
