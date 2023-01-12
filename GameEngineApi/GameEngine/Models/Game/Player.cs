using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
	
	public class Player
    {
		[ForeignKey("User")]
		public int Id { get; set; }
		public int Name { get; set; }
		public int CurrentBet { get; set; }
		public int Chips { get; set; }
        public List<Card> Cards { get; set; }
		public bool IsFolded { get; set; }

		public Gametable Gametable { get; set; }
		public virtual User User { get; set; }


	}
}
