using GameEngine.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameEngine.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine.Models.Game
{
	public class Card
	{
		public Symbols Symbol { get; set; }
		public CardTypes Type { get; set; }

		public IList<Player> Players { get; set; }
		public IList<PokerTable> Tables { get; set; }
		public IList<Deck> Decks { get; set; }
	}
}
