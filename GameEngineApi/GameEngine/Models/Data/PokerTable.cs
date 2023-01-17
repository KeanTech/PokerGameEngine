using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameEngine.Core.Services.Webhook;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine.Models.Game
{
    public class PokerTable
    {
		public int Id { get; set; }
        [ForeignKey("Player")]
        public int OwnerId { get; set; }
        public Player Owner { get; set; }
        public IList<Card> Cards { get; set; }
        public IList<Player> Players { get; set; }
        public IList<SubscribeModel> Subscriptions { get; set; }
        public Deck Deck { get; set; }
		public int Chips { get; set; }
    }
}
