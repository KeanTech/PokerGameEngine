using GameEngine.Core.Enums;

namespace GameEngine.Models.Game
{
    public class Card
    {
        public int Id { get; set; }
        public Symbols Symbol { get; set; }
        public CardTypes Type { get; set; }
    }
}
