using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine.Models.Game
{
    public class PokerTable
    {
		public int Id { get; set; }
        public int OwnerId { get; set; }
        public Player Owner { get; set; }
        public IList<Card> Cards { get; set; }
        public List<Player> Players { get; set; }
        public Deck Deck { get; set; }
		public int Chips { get; set; }
    }
}
