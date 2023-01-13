using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameEngine.Data;

namespace GameEngine.Models.Game
{
	
	public class Player
    {
		[ForeignKey("User")]
		public int Id { get; set; }
		public string Name { get; set; }
		public int CurrentBet { get; set; }
		public int Chips { get; set; }
        public IList<PlayerCard> Cards { get; set; }
		public bool IsFolded { get; set; }

    }
}
