using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine.Models.Game
{
    public class PokerTable
    {
        public int Id { get; set; }
        public IList<TableCard> Cards { get; set; }
        public IList<DeckCard>? CardDeck { get; set; }
        public IList<Player> Players { get; set; }
        public int ChipsValue { get; set; }
    }
}
