using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.Game
{
    public class GamePlayer
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CurrentBet { get; set; }
        public int ChipsValue { get; set; }
        public IList<PlayerCard> Cards { get; set; }
        public bool IsFolded { get; set; }

        public GamePlayer() 
        {
            
        }

       
    }
}
