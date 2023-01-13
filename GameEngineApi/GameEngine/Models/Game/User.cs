using System.ComponentModel.DataAnnotations;

namespace GameEngine.Models.Game
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserIdentifier { get; set; }
        public int Wins { get; set; }
        public IList<Accessory> Accessories { get; set; }
        public int ChipsAquired { get; set; }
        public Player Player { get; set; }
    }
}
