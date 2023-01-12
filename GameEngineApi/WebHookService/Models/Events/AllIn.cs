using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace WebHookService.Models.Events
{
	public class AllIn : PlayerEvent
	{
		public int AllInAmount { get; set; }
		public AllIn(string secret, int playerId, int allInAmount) : base(secret, Event.AllIn, playerId)
		{
			AllInAmount = allInAmount;
		}
	}
}
