using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameEngine.Core.Services.Webhook.Models.Events;
using Microsoft.EntityFrameworkCore;
using GameEngine.Models.Game;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

		public DbSet<GameEngine.Models.Game.User> User { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Player> Player { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.PokerTable> Table { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Accessory> Accessory { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Card> Card { get; set; } = default!;
    }
}
