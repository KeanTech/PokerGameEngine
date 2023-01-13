namespace GameEngine.Models.Game
{
	public class DeckCard
	{
		public int TableId { get; set; }
		public PokerTable PokerTable { get; set; }
		public int CardId { get; set; }
		public Card Card { get; set; }
	}
}
