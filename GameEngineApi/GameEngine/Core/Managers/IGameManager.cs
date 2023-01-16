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
        Task<PokerTable> StartNewGame(IList<Player> players, PokerTable pokerTable);

        /// <summary>
        /// This method clears the Table pool, playercards, playerBets, and tableCards
        /// </summary>
        /// <returns></returns>
        Task<PokerTable> ClearTable(PokerTable pokerTable);

        /// <summary>
        /// Needs a valid GameState to use this method
        /// <para>When this method is finished the GameState will be deleted from the database and the winning player gets stats</para>
        /// </summary>
        /// <param name="gameState"></param>
        void EndGame(PokerTable pokerTable);

        /// <summary>
        /// This method need a valid tableId
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns>A new GameState if theres is no GameState with that tableId
        /// <para>Else it returns the GameState from the database</para></returns>
        PokerTable GetCurrentGame(int tableId);
        
        /// <summary>
        /// This method will generate a new deck for the table
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns></returns>
        PokerTable GetNewCardDeck(PokerTable pokerTable);

        /// <summary>
        /// This method will update the gameState so that players have cards in their card List 
        /// </summary>
        /// <param name="amountOfCards"></param>
        /// <param name="gameState"></param>
        /// <returns></returns>
        Task<PokerTable> GiveCardsToPlayers(PokerTable pokerTable);

        /// <summary>
        /// This method will update the gameState so that players have cards in their card List 
        /// </summary>
        /// <param name="amountOfCards"></param>
        /// <param name="gameState"></param>
        /// <returns></returns>
        Task<PokerTable> GiveCardsToTable(int amountOfCards, PokerTable pokerTable);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<PokerTable> ResetPlayerBets(PokerTable pokerTable);
        Task<PokerTable> UpdateChipsPool(PokerTable pokerTable, int updateValue);
        Task<PokerTable> UpdateGameState(PokerTable pokerTable);
        bool PlayerCall(BetEvent betEvent);
        bool PlayerRaise(BetEvent betEvent);
        bool PlayerAllIn(BetEvent betEvent);
        bool PlayerFold(TurnEvent turnEvent);
        bool PlayerCheck(TurnEvent turnEvent);


    }
}