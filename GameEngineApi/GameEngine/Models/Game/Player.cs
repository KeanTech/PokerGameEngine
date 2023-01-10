using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
	
	public class Player : User
    {
        public int Id { get; set; }
        public int CurrentBet { get; set; }
		public Dictionary<int,int> Chips { get; set; }
        public List<Card> Cards { get; set; }

	}
}
