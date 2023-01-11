using GameEngine.Models.Events;
using GameEngine.Models.Game;
using System.Reflection;

namespace GameEngine.Core.Managers
{
    public interface IGameManager
    {
        GameState StartNewGame(int tabelId);
        GameState ClearTable();
        void EndGame(GameState gameState);
        GameState GetCurrentGame(int tableId);
        GameState GetNewCardDeck(GameState gameState);
        GameState GiveCards(int amountOfCards, GameState gameState);
        GameState ResetPlayerBet();
        void SetPlayerTurn(GameState gameState);
        GameState UpdateChipsPool();
        void PlayerCall(BetEvent betEvent);
        void PlayerRaise(BetEvent betEvent);
        void PlayerAllIn(BetEvent betEvent);
        void PlayerFold(int playerId, string userIdentifier);
        void PlayerCheck(int playerId, string userIdentifier);


    }
}