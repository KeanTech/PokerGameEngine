using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
    public class Accessory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
