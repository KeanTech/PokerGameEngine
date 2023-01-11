namespace GameEngine.Models.Game
{
    public class User
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string UserIdentifier { get; set; }
        public int Wins { get; set; }
        public List<Accessory> Accessories { get; set; }
        public int ChipsAquired { get; set; }
        public int TableId { get; set; }
    }
}
