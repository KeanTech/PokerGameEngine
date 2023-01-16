using GameEngine.Core.Enums;
using GameEngine.Models.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEngine.Models.BusinessModels
{
	public class PokerTableModel
	{
		public int Id { get; set; }
		public IList<CardModel> Cards { get; set; }
		public IList<CardModel> CardDeck { get; set; }
		public List<Player> Players { get; set; }
		public int Chips { get; set; }

		public PokerTableModel(PokerTable table)
		{
		}
	}

	public class CardModel
	{
		public int Id { get; set; }
		public Symbols Symbol { get; set; }
		public CardTypes Type { get; set; }

		public CardModel(Card card)
		{
			Symbol = card.Symbol;
			Type = card.Type;
		}

		public static CardModel CreateModel(Card card)
		{
			return new CardModel(card);
		}
	}

	public static class CardModelExtensions
	{
		public static List<CardModel> CreateModelList(this ICollection<Card> daoList)
		{
			return daoList.Select(CreateModel).ToList();
		}

		public static CardModel CreateModel(this Card dao)
		{
			return CardModel.CreateModel(dao);
		}
	}

	public class PlayerModel
	{
		[ForeignKey("User")]
		public int Id { get; set; }
		public string Name { get; set; }
		public int CurrentBet { get; set; }
		public int Chips { get; set; }
		public IList<CardModel> Cards { get; set; }
		public bool IsFolded { get; set; }
	}
}
