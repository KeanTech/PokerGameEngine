using GameEngine.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameEngine.Data;
using GameEngine.Models.BusinessModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine.Models.Game
{
    public class Card
    {
	    public Symbols Symbol { get; set; }
	    public CardTypes Type { get; set; }

		public IList<Player> Players { get; set; }
        public IList<PokerTable> Tables { get; set; }
        public IList<Deck> Decks { get; set; }

        public Card()
        {
	        
        }

        public Card(CardModel model)
        {
	        Symbol = model.Symbol;
	        Type = model.Type;
        }

        public static Card CreateDao(CardModel model)
        {
	        return new Card(model);
        }
	}

    public static class CardExtensions
    {
	    public static Card CreateDao(this CardModel model)
	    {
		    return Card.CreateDao(model);
	    }
    }
}
