using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebHookService
{
	public class WebhookContext : DbContext
	{
		public WebhookContext(DbContextOptions<WebhookContext> options) : base(options)
		{
		}

		public DbSet<SubscribeModel> Subscribe { get; set; }
	}
}
