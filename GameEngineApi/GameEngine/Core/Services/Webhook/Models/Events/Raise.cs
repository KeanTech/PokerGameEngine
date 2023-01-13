namespace GameEngine.Core.Services.Webhook.Models.Events
{
	public class Raise : PlayerEvent
	{
		public int RaiseAmount { get; set; }
		public int ChipsRemaining { get; set; }
		public Raise(string secret, int playerId, int raiseAmount, int chipsRemaining) : base(secret, Event.Raise, playerId)
		{
			RaiseAmount = raiseAmount;
			ChipsRemaining = chipsRemaining;
		}
	}
}
