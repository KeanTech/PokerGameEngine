using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameEngine.Data;

namespace GameEngine.Models.Game
{
	
	public class Player
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public int CurrentBet { get; set; }
		public int Chips { get; set; }
        public IList<Card> Cards { get; set; }
		public PokerTable Table { get; set; }
		public User User { get; set; }
		public bool IsFolded { get; set; }

    }
}
