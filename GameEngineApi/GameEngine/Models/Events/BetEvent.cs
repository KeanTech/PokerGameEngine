namespace GameEngine.Models.Events
{
    public class BetEvent : WebHookEvent
    {
        public int PlayerId { get; set; }
        public string UserIdentifier { get; set; }
        public int BetAmount { get; set; }
        public int PokerTableId { get; set; }
    }
}
