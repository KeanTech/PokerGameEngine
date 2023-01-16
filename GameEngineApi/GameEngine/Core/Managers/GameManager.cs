using GameEngine.Core.Enums;
using GameEngine.Core.Services.Webhook;
using GameEngine.Data;
using GameEngine.Models.Events;
using GameEngine.Models.Game;

namespace GameEngine.Core.Managers
{
    public class GameManager : IGameManager
    {
        private static List<Card> defaultCardDeck = new List<Card> 
        {
            #region Clubs     

            new Card() 
            {
                Symbol = Symbols.Club, Type = CardTypes.Two 
            },
            new Card()
            {
                Symbol = Symbols.Club, Type = CardTypes.Three
            }, 
            new Card()
            {
                Symbol = Symbols.Club, Type = CardTypes.Four
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Five
            },
            new Card()
            {
                Symbol = Symbols.Club, Type = CardTypes.Six
            }, 
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Seven
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Eight
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Nine
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Ten
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Pawn
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Queen
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.King
            },
            new Card()
            {
                Symbol= Symbols.Club, Type = CardTypes.Ace
            },
            #endregion

            #region Hearts

            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Two
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Three
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Four
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Five
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Six
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Seven
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Eight
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Nine
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Ten
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Pawn
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Queen
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.King
            },
            new Card()
            {
                Symbol= Symbols.Heart, Type = CardTypes.Ace
            },
            #endregion

            #region Spade
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Two
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Three
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Four
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Five
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Six
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Seven
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Eight
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Nine
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Ten
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Pawn
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Queen
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.King
                        },
                        new Card()
                        {
                            Symbol= Symbols.Spade, Type = CardTypes.Ace
                        },
            #endregion

            #region Diamond
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Two
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Three
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Four
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Five
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Six
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Seven
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Eight
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Nine
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Ten
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Pawn
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Queen
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.King
                                    },
                                    new Card()
                                    {
                                        Symbol= Symbols.Diamond, Type = CardTypes.Ace
                                    },
	            #endregion
        };
        
        private readonly GameEngineContext _context;
        private readonly IWebhookService _webhookService;

        public GameManager(GameEngineContext context, IWebhookService webhookService) 
        {
            _context = context;
            _webhookService = webhookService;
        }

        public static List<Card> GetNewCardDeck() => defaultCardDeck;

        public async Task<PokerTable> StartNewGame(IList<Player> players, PokerTable pokerTable) 
        {
            if (_context.Card == null || _context.Card.ToList().Count() == 0)
            {
                _context.Card.AddRange(GetNewCardDeck());
            } 

            pokerTable.Players = players;

            _context.Table.Update(pokerTable);
            await _context.SaveChangesAsync();

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
            pokerTable.ChipsValue += updateValue;
            await UpdateGameState(pokerTable);
            
            return pokerTable;
        }

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

        public async Task<PokerTable> GiveCardsToTable(int amountOfCards, PokerTable pokerTable) 
        {
            if (pokerTable.CardDeck != null && pokerTable.CardDeck.Count > 0)
            {
                Stack<Card> cards = new Stack<Card>(DataManager.GetCardsFromDeck(pokerTable.CardDeck));
                
                for (int i = 0; i < amountOfCards; i++)
                {
                    if (pokerTable.Cards == null)
                        pokerTable.Cards = new List<TableCard>();

                    pokerTable.Cards.Add(new TableCard() { TableId = pokerTable.Id, Card = cards.Pop() });
                }

                pokerTable.CardDeck = DataManager.GetDeckCards(cards.ToList(), pokerTable);

                await UpdateGameState(pokerTable);
            }

            return pokerTable;
        }

        public async Task<PokerTable> GiveCardsToPlayers(PokerTable pokerTable)
        {
            if (pokerTable.CardDeck != null && pokerTable.CardDeck.Count > 0)
            {
                Stack<Card> cards = new Stack<Card>(DataManager.GetCardsFromDeck(pokerTable.CardDeck));

                for (int i = 0; i < 2; i++)
                {
                    foreach (var player in pokerTable.Players)
                    {
                        int cardId = cards.Peek().Id;   
                        if(player.Cards == null)
                            player.Cards = new List<PlayerCard>();

                        player.Cards.Add(new PlayerCard() { PlayerId = player.Id, Card = cards.Pop(), CardId = cardId });
                    }
                }
                pokerTable.CardDeck = DataManager.GetDeckCards(cards.ToList(), pokerTable);

                await UpdateGameState(pokerTable);
            }

            return pokerTable;
        }

        public async Task<PokerTable> ClearTable(PokerTable pokerTable)
        {
            pokerTable.Cards?.Clear();
            pokerTable.CardDeck?.Clear();
            pokerTable.ChipsValue = 0;

            await RemovePlayerCards(pokerTable);
            await ResetPlayerBets(pokerTable);
            await UpdateGameState(pokerTable);

            return pokerTable;
        }
        
        public PokerTable GetNewCardDeck(PokerTable pokerTable)
        {
            if (_context.Card.ToList().Count == 0)
                _context.Card.AddRange(GetNewCardDeck());



            return pokerTable;
        }

        public void EndGame(PokerTable pokerTable)
        {
            GivePlayerStats(new User());
        }

        private void GivePlayerStats(User user)
        {
            user.Wins++;
            user.ChipsAquired += user.Player.ChipsValue;

            // save player(User)
        }

        /// <summary>
        /// Made for test reasons
        /// </summary>
        /// <param name="betEvent"></param>
        /// <param name="betEventType"></param>
        /// <returns></returns>
        public bool PlayerBet(BetEvent betEvent, string betType) 
        {
            GameState gameState = new GameState();
            Player? player = gameState.PokerTable.Players.FirstOrDefault(x => x.Id == betEvent.PlayerId);

            if (player != null)
            {
                if (player.ChipsValue < betEvent.BetAmount)
                    return false;

                player.CurrentBet = betEvent.BetAmount;
                player.ChipsValue -= betEvent.BetAmount;

                gameState.PokerTable.ChipsValue += betEvent.BetAmount;
                
                return true;
            }

            return false;
        }

        public bool PlayerTurnEvent(TurnEvent turnEvent, string turnType) 
        {
            GameState gameState = new GameState();
            Player? player = gameState.PokerTable.Players.FirstOrDefault(x => x.Id == turnEvent.PlayerId);
            int highestBet = GetHighestBet(gameState.PokerTable.Players);
            
            if (player != null)
            {
                switch (turnType)
                {
                    case "Fold":
                        player.IsFolded = true; 
                        return true;

                    case "Check":
                        if (player.CurrentBet == highestBet)
                        {
                            PlayerCheck(turnEvent);
                            return true;
                        }

                        return false;
                    
                    default:
                        return false;
                }
            }

            return false;
        }

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
    }
}
