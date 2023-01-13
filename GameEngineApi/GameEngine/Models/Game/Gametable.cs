using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine.Models.Game
{
    public class Gametable
    {
		public int Id { get; set; }
        public IList<TableCard> Cards { get; set; }
        public IList<DeckCard>? CardDeck { get; set; }
        public List<Player> Players { get; set; }
		public int Chips { get; set; }
    }

    public class TableCard
    {
        public int TableId { get; set; }
        public Gametable Gametable { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }

    public class DeckCard
    {
        public int TableId { get; set; }
        public Gametable Table {get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
