namespace GameEngine.Models.Events
{
    public class TurnEvent
    {
        public int PlayerId { get; set; }
        public string PlayerIdentifier { get; set; }
        public int PokerTableId { get; set; }
    }
}
