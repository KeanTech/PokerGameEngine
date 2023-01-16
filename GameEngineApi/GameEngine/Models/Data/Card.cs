using GameEngine.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameEngine.Data;

namespace GameEngine.Models.Game
{
    public class Card
    {
        public int Id { get; set; }
        public Symbols Symbol { get; set; }
        public CardTypes Type { get; set; }

		public IList<PlayerCard> Players { get; set; }
        public IList<TableCard> Tables { get; set; }
        public IList<DeckCard> Decks { get; set; }
	}
}
