using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
	
	public class Player : User
    {
        public int CurrentBet { get; set; }
		public int Chips { get; set; }
        public List<Card> Cards { get; set; }

	}
}
