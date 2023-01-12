namespace GameEngine.Core.Services.Webhook
{
	public class SubscribeModel
	{
		public int Id { get; set; }
		public int TableId { get; set; }
		public string CallbackUrl { get; set; }
		public string UserIdentifier { get; set; }
	}
}
