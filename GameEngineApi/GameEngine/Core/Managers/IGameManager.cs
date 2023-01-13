using GameEngine.Models.Events;
using GameEngine.Models.Game;
using System.Reflection;

namespace GameEngine.Core.Managers
{
    public interface IGameManager
    {
        /// <summary>
        /// There has to be a Table to run this method with players  
        /// <para>After this method has run the table has cards and the players on the table also has been given cards</para>
        /// <para>Then it pings the clients subscriped to the table so they can see whos turn it is</para>
        /// </summary>
        /// <param name="tabelId"></param>
        /// <returns></returns>
        GameState StartNewGame(int tabelId);

        /// <summary>
        /// This method clears the Table pool, playercards, playerBets, and tableCards
        /// </summary>
        /// <returns></returns>
        GameState ClearTable(GameState gameState);

        /// <summary>
        /// Needs a valid GameState to use this method
        /// <para>When this method is finished the GameState will be deleted from the database and the winning player gets stats</para>
        /// </summary>
        /// <param name="gameState"></param>
        void EndGame(GameState gameState);

        /// <summary>
        /// This method need a valid tableId
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns>A new GameState if theres is no GameState with that tableId
        /// <para>Else it returns the GameState from the database</para></returns>
        GameState GetCurrentGame(int tableId);
        
        /// <summary>
        /// This method will generate a new deck for the table
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns></returns>
        GameState GetNewCardDeck(GameState gameState);

        /// <summary>
        /// This method will update the gameState so that players have cards in their card List 
        /// </summary>
        /// <param name="amountOfCards"></param>
        /// <param name="gameState"></param>
        /// <returns></returns>
        GameState GiveCards(int amountOfCards, GameState gameState);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GameState ResetPlayerBets(GameState gameState);
        void SetPlayerTurn(GameState gameState);
        GameState UpdateChipsPool(GameState gameState, int updateValue);
        GameState UpdateGameState(GameState gameState);
        bool PlayerCall(BetEvent betEvent);
        bool PlayerRaise(BetEvent betEvent);
        bool PlayerAllIn(BetEvent betEvent);
        bool PlayerFold(TurnEvent turnEvent);
        bool PlayerCheck(TurnEvent turnEvent);


    }
}