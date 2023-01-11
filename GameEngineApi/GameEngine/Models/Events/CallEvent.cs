namespace GameEngine.Models.Events
{
    public class CallEvent : WebHookEvent
    {
        public int PlayerId { get; set; }
        public int CallAmount { get; set; }
        public int PokerTableId { get; set; }
    }
}
