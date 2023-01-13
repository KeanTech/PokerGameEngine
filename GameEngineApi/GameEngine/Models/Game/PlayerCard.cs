namespace GameEngine.Models.Game
{
	public class PlayerCard
	{
		public int PlayerId { get; set; }
		public Player Player { get; set; }
		public int CardId { get; set; }
		public Card Card { get; set; }
	}
}
