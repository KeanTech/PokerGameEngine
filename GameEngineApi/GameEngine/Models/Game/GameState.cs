﻿namespace GameEngine.Models.Game
{
    public class GameState
    {
        public PokerTable PokerTable { get; set; }
        public int CurrentPlayerId { get; set; }
        public string PlayerIdentifier { get; set; }
    }
}
