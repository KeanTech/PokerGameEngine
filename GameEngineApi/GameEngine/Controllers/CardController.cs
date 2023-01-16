using GameEngine.Core.Managers;
using GameEngine.Models.Game;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        public CardController() { }

        private CardManager cardManager = new CardManager();

        [HttpGet("CheckHand")]
        public void CheckHand()
        {
            List<Card> playerCards = new List<Card>()
            {
                new Card()
                {
                    Type = Core.Enums.CardTypes.Ten,
                    Symbol = Core.Enums.Symbols.Heart,
                },
                new Card()
                {
                    Type = Core.Enums.CardTypes.Pawn,
                    Symbol= Core.Enums.Symbols.Heart,
                }
            };
            List<Card> boardCards = new List<Card>()
            {
                new Card()
                {
                    Type = Core.Enums.CardTypes.Two,
                    Symbol = Core.Enums.Symbols.Diamond,
                },
                new Card()
                {
                    Type = Core.Enums.CardTypes.Ace,
                    Symbol = Core.Enums.Symbols.Heart,
                },
                new Card()
                {
                    Type = Core.Enums.CardTypes.King,
                    Symbol = Core.Enums.Symbols.Heart,
                },
                new Card()
                {
                    Type = Core.Enums.CardTypes.Queen,
                    Symbol = Core.Enums.Symbols.Heart,
                },
                new Card()
                {
                    Type = Core.Enums.CardTypes.Seven,
                    Symbol = Core.Enums.Symbols.Club,
                }
            };
            //cardManager.ValidatePlayerHand(sortedList);
            cardManager.GetPlayerHandValue(playerCards, boardCards);
        }
    }
}
