using System.ComponentModel.DataAnnotations.Schema;
using GameEngine.Models.Game;

namespace GameEngine.Core.Services.Webhook
{
	public class SubscribeModel
	{
		public int Id { get; set; }
		[ForeignKey("PokerTable")]
		public int TableId { get; set; }
		public string CallbackUrl { get; set; }
		public string UserSecret { get; set; }
		public PokerTable Table { get; set; }
	}
}
