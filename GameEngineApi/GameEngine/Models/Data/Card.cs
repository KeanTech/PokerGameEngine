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
	public static class CardExtention
	{
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
