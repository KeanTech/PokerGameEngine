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
        bool PlayerCall(BetEvent betEvent);
        bool PlayerRaise(BetEvent betEvent);
        bool PlayerAllIn(BetEvent betEvent);
        bool PlayerFold(int playerId, string userIdentifier);
        bool PlayerCheck(int playerId, string userIdentifier);


    }
}