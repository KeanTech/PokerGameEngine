namespace GameEngine.Models.Events
{
    public class TurnEvent : WebHookEvent
    {
        public int PlayerId { get; set; }
        public string PlayerIdentifier { get; set; }
        public int PokerTableId { get; set; }
    }
}
