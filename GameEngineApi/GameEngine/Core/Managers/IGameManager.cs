using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public interface IGameManager
    {
        GameState ClearTable();
        void EndGame(GameState gameState);
        void GetCurrentGame(GameState gameState);
        GameState GetNewCardDeck(GameState gameState);
        GameState GiveCards(int amountOfCards);
        GameState ResetPlayerBet();
        void SetPlayerTurn();
        GameState UpdateChipsPool();
    }
}