namespace GameEngine.Core.Services.Webhook.Models
{
	public enum Event
	{
		Call,
		Raise,
		Check,
		AllIn,
		Fold,
		PlayerCards,
		TableCards,
		GameState,
		PlayerJoined,
		PlayerLeft,
		GameStart,
		RoundEnd,
		PlayerReady,
		TableCreated,
		PlayerTurn
	}
}
