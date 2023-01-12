using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHookService
{
	public class SubscribeModel
	{
		public int Id { get; set; }
		public int TableId { get; set; }
		public string CallbackUrl { get; set; }
		public string UserIdentifier { get; set; }
	}
}
