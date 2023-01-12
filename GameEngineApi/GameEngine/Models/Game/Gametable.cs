using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
    public class Gametable
    {
		public int Id { get; set; }
        public List<Card> Cards { get; set; }
        public Stack<Card> CardDeck { get; set; }
        public List<Player> Players { get; set; }
		public int Chips { get; set; }
    }
}
