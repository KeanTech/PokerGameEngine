using Microsoft.EntityFrameworkCore;

namespace GameEngine.Core.Services.Webhook
{
	public class WebhookContext : DbContext
	{
		public WebhookContext(DbContextOptions<WebhookContext> options) : base(options)
		{
		}

		public DbSet<SubscribeModel> Subscribe { get; set; }
	}
}
