using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHookService.Models.Events
{
	public class PlayerEvent : WebhookEvent
	{
		public PlayerEvent(string secret, Event eventType, int playerId) : base(secret, eventType)
		{
			PlayerId = playerId;
		}
		public int PlayerId { get; set; }
	}
}
