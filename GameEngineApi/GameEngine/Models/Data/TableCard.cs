namespace GameEngine.Models.Game
{
	public class TableCard
	{
		public int TableId { get; set; }
		public PokerTable PokerTable { get; set; }
		public int CardId { get; set; }
		public Card Card { get; set; }
	}
}
