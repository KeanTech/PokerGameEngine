namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class AllIn : PlayerEvent
	{
		public int AllInAmount { get; set; }
		public AllIn(string secret, int playerId, int allInAmount) : base(secret, Event.AllIn, playerId)
		{
			AllInAmount = allInAmount;
		}
	}
}
