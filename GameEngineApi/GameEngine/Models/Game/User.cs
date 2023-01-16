using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }
        public List<Accessory> Accessories { get; set; }
        public int ChipsAquired { get; set; }
        public string UserSecret { get; set; }
    }
}
