using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

			modelBuilder.Entity<Gametable>()
				.HasMany(g => g.Cards)
				.WithMany(c => c.Gametables);

			modelBuilder.Entity<Gametable>()
				.HasMany(g => g.CardDeck)
				.WithMany(c => c.Gametables);

			modelBuilder.Entity<Player>()
				.HasOne(p => p.Gametable)
				.WithMany(g => g.Players);
				
		
		}

		public DbSet<GameEngine.Models.Game.User> User { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Player> Player { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Gametable> Table { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Accessory> Accessory { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Chip> Chip { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Card> Card { get; set; } = default!;
	}
}
