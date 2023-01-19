using GameEngine.Core.Enums;
using GameEngine.Core.Services.Webhook;
using GameEngine.Core.Services.Webhook.Models.Events;
using GameEngine.Core.Services.Webhook.Models;
using GameEngine.Data;
using GameEngine.Models.Events;
using GameEngine.Models.Game;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task<PokerTable> StartNewGame(int pokerTableId, int amountOfStartingChips)
        {
            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == pokerTableId);

            if (pokerTable == null)
                return new PokerTable();

            await GiveCardsToPlayers(pokerTable);
            await GiveCardsToTable(3, pokerTable);

            GiveStartingChipsToPlayers(pokerTable, amountOfStartingChips);
            _context.Table.Update(pokerTable);
            await _context.SaveChangesAsync();

            Player startingPlayer = pokerTable.Players.First();
            foreach (var player in pokerTable.Players)
            {
                // The player gets notification about 
                WebhookEvent webhookEvent = CreateGameStartEvent(player, pokerTable);
                NotifyPlayer(webhookEvent, pokerTable.Id);
            }

            // Send notification to the starting player. so the player knows it is their turn
            WebhookEvent playerEvent = CreatePlayerEvent(startingPlayer, Event.PlayerTurn, startingPlayer.Id);
            NotifyPlayer(playerEvent, startingPlayer.Id);
            
            return pokerTable;
        }

        public async void StartRound(PokerTable pokerTable, int entryBet)
        {
            await GetNewCardDeck(pokerTable);
            await GiveCardsToPlayers(pokerTable);
            await GiveCardsToTable(1, pokerTable);


            foreach (var player in pokerTable.Players)
            {
                if (player.Chips < entryBet && player.Chips > 0)
                {
                    player.CurrentBet = player.Chips;
                    pokerTable.Chips += player.CurrentBet;

                    AllIn playerEvent = CreateAllIn(player, player.Chips);
                    NotifyPlayers(playerEvent, pokerTable.Id);

                    player.Chips = 0;
                }
                else
                {
                    player.Chips -= entryBet;
                    pokerTable.Chips += entryBet;
                }
            }

        }

        public async void EndRound(PokerTable pokerTable)
        {
            await GiveChipsToWinningPlayers(pokerTable);
            await ClearTable(pokerTable);

            foreach (var player in pokerTable.Players)
            {
                if (player.Chips == 0)
                {
                    PlayerEvent playerEvent = new PlayerEvent(player.User.UserSecret, Event.PlayerLeft, player.Id);
                    await _webhookService.NotifySubscribersOfPlayerEvent(playerEvent, pokerTable.Id);
                }
            }
        }

        public bool EndGame(PokerTable pokerTable)
        {
            if (pokerTable == null)
                return false;

            Player? player = pokerTable.Players.FirstOrDefault(x => x.Chips > 0);

            if (player == null)
                return false;

            GivePlayerStats(player);

            _context.Table.Remove(pokerTable);
            _context.SaveChanges();
            
            return true;
        }

        public bool CreateNewPokerTable(User userCreatingTheTable) 
        {
            PokerTable pokerTable = new PokerTable();

            if (pokerTable.Deck == null)
                pokerTable.Deck = new Deck();

            if (pokerTable.Deck.Cards == null)
                pokerTable.Deck.Cards = new List<Card>();

            pokerTable.Deck.Cards = _context.Card.ToList();
            if (pokerTable.Deck.Cards.Count <= 0)
                return false;

            Player player = new Player();
            player = new Player() { User = userCreatingTheTable, Cards = new List<Card>() };
            _context.Player.Add(player);
            _context.SaveChanges();
            pokerTable.Owner = player;
            pokerTable.OwnerId = player.Id;


            _context.Table.Add(pokerTable);
            _context.SaveChanges();

            player.Table = pokerTable;
            _context.Update(player);
            _context.SaveChanges();

            WebhookEvent webhookEvent = new WebhookEvent(userCreatingTheTable.UserSecret, Event.TableCreated);
            NotifyPlayer(webhookEvent, pokerTable.Id);

            return true;
        }

        public bool JoinTable(User user, PokerTable pokerTable) 
        {
            Player player = new Player();
            player = new Player() { User = user, Cards = new List<Card>() };
            _context.Player.Add(player);
            _context.SaveChanges();

            pokerTable.Players.Add(player);

            foreach (var pokerPlayer in pokerTable.Players)
            {
                PlayerEvent webhookEvent = CreatePlayerEvent(pokerPlayer, Event.PlayerJoined, player.Id);
                NotifyPlayer(webhookEvent, pokerTable.Id);
            }

            _context.SaveChanges();

            return true;
        }

        public bool LeaveTable(Player player, PokerTable pokerTable) 
        {
            foreach (var pokerPlayer in pokerTable.Players)
            {
                PlayerEvent playerEvent = CreatePlayerEvent(pokerPlayer, Event.PlayerLeft, player.Id);
                NotifyPlayer(playerEvent, pokerTable.Id);
            }

            pokerTable.Players.Remove(player);
            _context.SaveChanges();
            
            return true;
        }


        #region Private methods

        #region Notify methods
        private async void NotifyPlayers(WebhookEvent webhookEvent, int tableId)
        {
            await _webhookService.NotifySubscriberOfStateEvent(webhookEvent.Secret, tableId, webhookEvent);
        }

        private async void NotifyPlayer(WebhookEvent webhookEvent, int tableId)
        {
            await _webhookService.NotifySubscriberOfStateEvent(webhookEvent.Secret, tableId, webhookEvent);
        }

        #endregion

        #region Create evnets
        private PlayerCard CreatePlayerCardEvent(Player player, List<Card> cards)
        {
            return new PlayerCard(player.User.UserSecret, cards);
        }

        private GameStart CreateGameStartEvent(Player player, PokerTable pokerTable) 
        {
            return new GameStart(player.User.UserSecret, pokerTable, player.Cards);
        }

        private TableCard CreateTableCardEvent(Player player, List<Card> cards)
        {
            return new TableCard(cards, player.User.UserSecret);
        }

        private RoundEnd CreateRoundEndEvent(Player player, PokerTable pokerTable)
        {
            return new RoundEnd(player.User.UserSecret, pokerTable);
        }

        private RoundStart CreateRoundStartEvent(Player player, PokerTable pokerTable)
        {
            return new RoundStart(player.User.UserSecret, pokerTable);
        }

        private Raise CreateRaiseEvent(Player player, int raiseAmount, int chipsRemaining)
        {
            return new Raise(player.User.UserSecret, player.Id, raiseAmount, chipsRemaining);
        }

        private Call CreateCallEvent(Player player, int callAmount, int chipsRemaining)
        {
            return new Call(callAmount, chipsRemaining, player.Id, player.User.UserSecret);
        }

        private AllIn CreateAllIn(Player player, int allInAmount)
        {
            return new AllIn(player.User.UserSecret, player.Id, allInAmount);
        }

        private PlayerEvent CreatePlayerEvent(Player player, Event eventType, int playerId)
        {
            return new PlayerEvent(player.User.UserSecret, eventType, playerId);
        }

        #endregion

        private PokerTable MakePokerTableReadyForEvent(PokerTable pokerTable)
        {
            //Remove unrelated table stuff
            pokerTable.Deck.Cards.Clear();
            pokerTable.Subscriptions.Clear();

            //Remove unrelated ower stuff
            pokerTable.Owner.Cards.Clear();
            pokerTable.Owner.Table = new PokerTable();
            pokerTable.Owner.User = new User();

            foreach (var player in pokerTable.Players)
            {
                player.Cards.Clear();
                player.Table = new PokerTable();
                player.User = new User();
            }

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

                for (int i = playerIndex; i < pokerTable.Players.Count; i++)
                {
                    if (pokerTable.Players[i].IsFolded == false && pokerTable.Players[i].Chips > 0)
                        return pokerTable.Players[i];
                }
            }

            Player nextPlayer = pokerTable.Players[playerIndex + 1];

            if (nextPlayer.IsFolded)
            {
                playerIndex = pokerTable.Players.IndexOf(nextPlayer);

                for (int i = playerIndex; i < pokerTable.Players.Count; i++)
                {
                    if (pokerTable.Players[i].IsFolded == false && pokerTable.Players[i].Chips > 0)
                        return pokerTable.Players[i];
                }
            }

            return pokerTable.Players[playerIndex + 1];
        }

        private bool IsLastPlayer(PokerTable pokerTable, int currentPlayerId)
        {
            int lastPlayerId = pokerTable.Players.Last().Id;
            if (lastPlayerId == currentPlayerId)
                return true;

            return false;
        }

        public PokerTable GetCurrentGame(int tableId)
        {
            PokerTable? pokerTable = _context.Table.First(x => x.Id == tableId);
            return pokerTable;
        }

        private async Task<PokerTable> UpdateGameState(PokerTable pokerTable)
        {
            if (pokerTable != null)
            {
                _context.Table.Update(pokerTable);
                await _context.SaveChangesAsync();
                return pokerTable;
            }

            return new PokerTable();
        }

        private async Task<PokerTable> UpdateChipsPool(PokerTable pokerTable, int updateValue)
        {
            pokerTable.Chips += updateValue;
            await UpdateGameState(pokerTable);

            return pokerTable;
        }

        #region Give Cards/Chips methods

        private async Task<PokerTable> GiveCardsToTable(int amountOfCards, PokerTable pokerTable)
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

        private async Task<PokerTable> GiveCardsToPlayers(PokerTable pokerTable)
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

        private PokerTable GiveStartingChipsToPlayers(PokerTable pokerTable, int startingChipAmount)
        {
            foreach (var player in pokerTable.Players)
            {
                player.Chips = startingChipAmount;
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


        private async Task<PokerTable> GiveChipsToWinningPlayers(PokerTable pokerTable)
        {
            // implement CardManager to determine the winners

            await UpdateGameState(pokerTable);
            return pokerTable;
        }

        /// <summary>
        /// There needs to be cards in the db for this to work
        /// <para>After getting cards from the database it will shuffle the list so it will never have the same order</para>
        /// </summary>
        /// <param name="pokerTable"></param>
        /// <returns>Shuffled <see cref="PokerTable.Cards"/> list</returns>
        private async Task<PokerTable> GetNewCardDeck(PokerTable pokerTable)
        {
            List<Card> cards = await _context.Card.ToListAsync();

            for (int i = 0; i < 5; i++)
            {
                cards.Shuffle();
            }

            return pokerTable;
        }

        private void GivePlayerStats(Player player)
        {
            player.User.Wins++;
            player.User.ChipsAquired += player.Chips;

            _context.Player.Update(player);
            _context.SaveChanges(); 
        }

        /// <summary>
        /// Not done yet need notifiers 
        /// </summary>
        /// <param name="betEvent"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        private bool PlayerBet(BetEvent betEvent, string betType)
        {
            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == betEvent.PokerTableId);

            if (pokerTable == null)
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

        /// <summary>
        /// Not done yet need notifiers 
        /// </summary>
        /// <param name="turnEvent"></param>
        /// <param name="turnType"></param>
        /// <param name="entryBet"></param>
        /// <returns></returns>
        private bool PlayerTurnEvent(TurnEvent turnEvent, string turnType, int entryBet)
        {
            PokerTable? pokerTable = _context.Table.FirstOrDefault(x => x.Id == turnEvent.PokerTableId);

            if (pokerTable == null)
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

        #endregion

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

        public bool PlayerFold(TurnEvent turnEvent, int entryBet = 500)
        {
            return PlayerTurnEvent(turnEvent, "Fold", entryBet);
        }

        public bool PlayerCheck(TurnEvent turnEvent, int entryBet = 500)
        {
            return PlayerTurnEvent(turnEvent, "Check", entryBet);
        }

        #endregion

    }
}
