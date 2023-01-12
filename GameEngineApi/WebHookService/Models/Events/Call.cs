namespace WebHookService.Models.Events
{
	public class Call : PlayerEvent
	{
		public Call(int callAmount, int chipsRemaining, int playerId, string secret) : base(secret, Event.Call, playerId)
		{
			CallAmount = callAmount;
			ChipsRemaining = chipsRemaining;
		}

		public int CallAmount { get; set; }
		public int ChipsRemaining { get; set; }
	}
}
