using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public interface IGameManager
    {
        public GameState StartNewGame(int tabelId);
        GameState ClearTable();
        void EndGame(GameState gameState);
        GameState GetCurrentGame(int tableId);
        GameState GetNewCardDeck(GameState gameState);
        GameState GiveCards(int amountOfCards, GameState gameState);
        GameState ResetPlayerBet();
        void SetPlayerTurn();
        GameState UpdateChipsPool();

    }
}