using Microsoft.EntityFrameworkCore;
using GameEngine.Models.Game;
using GameEngine.Core.Services.Webhook;

namespace GameEngine.Data
{
    public class GameEngineContext : DbContext
    {
        public GameEngineContext (DbContextOptions<GameEngineContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Card>().HasKey(e =>
			new {
				e.Symbol, e.Type
			});
			modelBuilder.Entity<PokerTable>().HasOne(e => e.Owner).WithOne(e => e.Table);

		}

		public DbSet<User> User { get; set; } = default!;
		public DbSet<Player> Player { get; set; } = default!;
		public DbSet<PokerTable> Table { get; set; } = default!;
		public DbSet<Accessory> Accessory { get; set; } = default!;
		public DbSet<Card> Card { get; set; } = default!;
		public DbSet<SubscribeModel> Subscribe { get; set; }
    }
}
