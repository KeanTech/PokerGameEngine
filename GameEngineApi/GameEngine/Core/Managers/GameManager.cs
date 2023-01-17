using GameEngine.Core.Enums;
using GameEngine.Core.Services.Webhook;
using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Core.Services.Webhook.Models;
using GameEngine.Data;
using GameEngine.Models.Events;
using GameEngine.Models.Game;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine.Core.Managers
{
    public class GameManager : IGameManager
    {
        private readonly GameEngineContext _context;
        private readonly IWebhookService _webhookService;

        public GameManager(GameEngineContext context, IWebhookService webhookService) 
        {
            _context = context;
            _webhookService = webhookService;
        }

        public async Task<PokerTable> StartNewGame(PokerTable pokerTable, int amountOfStartingChips) 
        {
            await GiveCardsToPlayers(pokerTable);
            await GiveCardsToTable(3, pokerTable);

            foreach (var player in pokerTable.Players)
            {
                player.Chips = amountOfStartingChips;
            }

            _context.Table.Update(pokerTable);
            await _context.SaveChangesAsync();


            PlayerEvent playerEvent = new PlayerEvent(pokerTable.Owner.User.UserSecret, Event.GameStart, pokerTable.Id);
            await _webhookService.NotifySubscribersOfPlayerEvent(playerEvent, pokerTable.Id);

            Player startingPlayer = pokerTable.Players.First();
            WebhookEvent webhookEvent = new WebhookEvent("", Event.PlayerTurn);
            await _webhookService.NotifySubscriberOfStateEvent(startingPlayer.User.UserSecret, pokerTable.Id, webhookEvent);

            // Make call to webhookService !!

            return pokerTable;   
        }

        private int GetHighestBet(IList<Player> players) 
        {
            int highestBet = players.Max(x => x.CurrentBet);

            return highestBet;
        }
        private Player? FindNextPlayer(PokerTable pokerTable, int currentPlayerId)
        {
            Player? currentPlayer = pokerTable.Players.FirstOrDefault(x => x.Id == currentPlayerId);

            if (currentPlayer == null)
                return null;

            int playerIndex = pokerTable.Players.IndexOf(currentPlayer);
            // make check for fold

            if (playerIndex == pokerTable.Players.Count - 1)
            {
                Player firstPlayer = pokerTable.Players.First();

                if (firstPlayer.IsFolded == false)
                    return firstPlayer;

                playerIndex = pokerTable.Players.IndexOf(firstPlayer);
                
                // Make method for this 
                for (int i = playerIndex; i < pokerTable.Players.Count; i++)
                {
                    if (pokerTable.Players[i].IsFolded == false)
                        return pokerTable.Players[i];
                }
            }

            Player nextPlayer = pokerTable.Players[playerIndex + 1];

            if (nextPlayer.IsFolded)
            {
                playerIndex = pokerTable.Players.IndexOf(nextPlayer);

                // Make method for this 
                for (int i = playerIndex; i < pokerTable.Players.Count; i++)
                {
                    if (pokerTable.Players[i].IsFolded == false)
                        return pokerTable.Players[i];
                }
            }

            return pokerTable.Players[playerIndex + 1];
        }

        private bool IsLastPlayer(PokerTable pokerTable, int currentPlayerId) 
        {
            int lastPlayerId = pokerTable.Players.Last().Id;
            if(lastPlayerId == currentPlayerId)
                return true;

            return false;
        }

        public PokerTable GetCurrentGame(int tableId)
        {
            PokerTable? pokerTable = _context.Table.First(x => x.Id == tableId);
            return pokerTable;
        } 

        public async Task<PokerTable> UpdateGameState(PokerTable pokerTable)
        {
            if (pokerTable != null)
            { 
                _context.Table.Update(pokerTable);
                await _context.SaveChangesAsync();
                return pokerTable;
            }    

            return new PokerTable();
        }

        public async Task<PokerTable> UpdateChipsPool(PokerTable pokerTable, int updateValue)
        {
            pokerTable.Chips += updateValue;
            await UpdateGameState(pokerTable);
            
            return pokerTable;
        }

        #region Give Cards methods
        public async Task<PokerTable> GiveCardsToTable(int amountOfCards, PokerTable pokerTable)
        {
            if (pokerTable.Deck != null && pokerTable.Deck.Cards.Count > 0)
            {
                Stack<Card> cards = new Stack<Card>(pokerTable.Deck.Cards);

                for (int i = 0; i < amountOfCards; i++)
                {
                    if (pokerTable.Cards == null)
                        pokerTable.Cards = new List<Card>();

                    pokerTable.Cards.Add(cards.Pop());
                }

                pokerTable.Cards = cards.ToList();

                await UpdateGameState(pokerTable);
            }

            return pokerTable;
        }

        public async Task<PokerTable> GiveCardsToPlayers(PokerTable pokerTable)
        {
            if (pokerTable.Deck.Cards != null && pokerTable.Deck.Cards.Count > 0)
            {
                Stack<Card> cards = new Stack<Card>(pokerTable.Deck.Cards);

                for (int i = 0; i < 2; i++)
                {
                    foreach (var player in pokerTable.Players)
                    {
                        if (player.Cards == null)
                            player.Cards = new List<Card>();

                        player.Cards.Add(cards.Pop());
                    }
                }
                pokerTable.Deck.Cards = cards.ToList();

                await UpdateGameState(pokerTable);
            }

            return pokerTable;
        }

        #endregion


        #region Clear/Reset methods

        public async Task<PokerTable> RemovePlayerCards(PokerTable pokerTable)
        {
            foreach (var player in pokerTable.Players)
            {
                player.Cards.Clear();
            }

            await UpdateGameState(pokerTable);

            return pokerTable;
        }
        public async Task<PokerTable> ResetPlayerBets(PokerTable pokerTable)
        {
            foreach (var player in pokerTable.Players)
            {
                player.CurrentBet = 0;
            }

            await UpdateGameState(pokerTable);

            return pokerTable;
        }

        public async Task<PokerTable> ClearTable(PokerTable pokerTable)
        {
            pokerTable.Cards?.Clear();
            pokerTable.Deck.Cards?.Clear();
            pokerTable.Chips = 0;

            await RemovePlayerCards(pokerTable);
            await ResetPlayerBets(pokerTable);
            await UpdateGameState(pokerTable);

            return pokerTable;
        }

        #endregion

        public PokerTable GetNewCardDeck(PokerTable pokerTable)
        {
            // Get Cards from database

            return pokerTable;
        }

        public void EndRound(PokerTable pokerTable) 
        {
            
        }

        public bool EndGame(PokerTable pokerTable)
        {
            if (pokerTable == null)
                return false;

            Player? player = pokerTable.Players.FirstOrDefault(x => x.Chips > 0);
            
            if(player == null)
                return false;

            GivePlayerStats(player);

            // Delete data from db
            return true;
        }

        private void GivePlayerStats(Player player)
        {
            player.User.Wins++;
            player.User.ChipsAquired += player.Chips;

            _context.Player.Update(player);
        }

        /// <summary>
        /// Made for test reasons
        /// </summary>
        /// <param name="betEvent"></param>
        /// <param name="betEventType"></param>
        /// <returns></returns>
        public bool PlayerBet(BetEvent betEvent, string betType) 
        {
            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == betEvent.PokerTableId);
            
            if(pokerTable == null)
                return false;

            Player? player = pokerTable.Players.FirstOrDefault(x => x.Id == betEvent.PlayerId);
            if (player != null)
            {
                if (player.Chips < betEvent.BetAmount)
                    return false;

                switch (betType)
                {
                    case "Call":
                        int highestBet = GetHighestBet(pokerTable.Players);
                        player.Chips -= (highestBet - player.CurrentBet);
                        player.CurrentBet = highestBet;
                        return true;

                    default:
                        break;
                }

                player.CurrentBet += betEvent.BetAmount;
                player.Chips -= betEvent.BetAmount;

                pokerTable.Chips += betEvent.BetAmount;
                
                return true;
            }

            return false;
        }

        public bool PlayerTurnEvent(TurnEvent turnEvent, string turnType) 
        {
            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == turnEvent.PokerTableId);
            
            if(pokerTable == null)
                return false;

            Player? player = pokerTable.Players.FirstOrDefault(x => x.Id == turnEvent.PlayerId);
            
            if (player != null)
            {
                int highestBet = GetHighestBet(pokerTable.Players);
                
                switch (turnType)
                {
                    case "Fold":
                        player.IsFolded = true; 
                        return true;

                    case "Check":
                        if (player.CurrentBet == highestBet)
                        {
                            PlayerCheck(turnEvent);
                            if (IsLastPlayer(pokerTable, turnEvent.PlayerId))
                                EndRound(pokerTable);

                            return true;
                        }

                        return false;
                    
                    default:
                        return false;
                }
            }

            return false;
        }

        #region Player Events

        public bool PlayerCall(BetEvent betEvent)
        {
            return PlayerBet(betEvent, "Call");
        }

        public bool PlayerRaise(BetEvent betEvent)
        {
            return PlayerBet(betEvent, "Raise");
        }

        public bool PlayerAllIn(BetEvent betEvent)
        {
            return PlayerBet(betEvent, "AllIn");
        }

        public bool PlayerFold(TurnEvent turnEvent)
        {
            return PlayerTurnEvent(turnEvent, "Fold");
        }

        public bool PlayerCheck(TurnEvent turnEvent)
        {
            return PlayerTurnEvent(turnEvent, "Check");
        }

        #endregion

    }
}
