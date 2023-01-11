namespace GameEngine.Models.Game
{
    public class Table
    {
        public int Id { get; set; }
        public List<Card> Cards { get; set; }
        public List<Card> CardDeck { get; set; }
		public int Chips { get; set; }
        public List<Player> Players { get; set; }

    }
}
