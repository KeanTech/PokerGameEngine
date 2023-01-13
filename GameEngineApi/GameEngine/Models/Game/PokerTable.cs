﻿namespace GameEngine.Models.Game
{
    public class PokerTable
    {
        public int Id { get; set; }
        public Stack<Card> Cards { get; set; }
        public Stack<Card> CardDeck { get; set; }
		public int ChipsValue { get; set; }
        public List<Player> Players { get; set; }

    }
}
