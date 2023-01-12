using GameEngine.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
    public class Card
    {
        public int Id { get; set; }
        public Symbols Symbol { get; set; }
        public CardTypes Type { get; set; }

        public List<Gametable> Gametables{ get; set; }
		public List<Player> Players { get; set; }
	}
}
