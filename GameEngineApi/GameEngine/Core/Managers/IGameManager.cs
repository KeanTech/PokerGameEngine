using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Core.Services.Webhook.Models;
using GameEngine.Models.Events;
using GameEngine.Models.Game;
using Microsoft.EntityFrameworkCore;
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
        Task<PokerTable> StartNewGame(int pokerTableId, int amountOfStartingChips);

        void StartRound(PokerTable pokerTable, int entryBet);
        void EndRound(PokerTable pokerTable);
        /// <summary>
        /// Needs a valid GameState to use this method
        /// <para>When this method is finished the GameState will be deleted from the database and the winning player gets stats</para>
        /// </summary>
        /// <param name="gameState"></param>
        bool EndGame(PokerTable pokerTable);
        public bool CreateNewPokerTable(User userCreatingTheTable);
        public bool JoinTable(User user, PokerTable pokerTable);
        public bool LeaveTable(Player player, PokerTable pokerTable);

        /// <summary>
        /// This method need a valid tableId
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns>A new GameState if theres is no GameState with that tableId
        /// <para>Else it returns the GameState from the database</para></returns>
        PokerTable GetCurrentGame(int tableId);
        bool PlayerCall(BetEvent betEvent);
        bool PlayerRaise(BetEvent betEvent);
        bool PlayerAllIn(BetEvent betEvent);
        bool PlayerFold(TurnEvent turnEvent, int entryBet = 500);
        bool PlayerCheck(TurnEvent turnEvent, int entryBet = 500);
    }
}