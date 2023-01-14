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

			modelBuilder.Entity<PlayerCard>().HasKey(pc => new { pc.CardId, pc.PlayerId });
			modelBuilder.Entity<TableCard>().HasKey(tc => new { tc.CardId, tc.TableId });
			modelBuilder.Entity<DeckCard>().HasKey(tc => new { tc.CardId, tc.TableId });


			//Table card relations
			modelBuilder.Entity<TableCard>()
				.HasOne<PokerTable>(tc => tc.PokerTable)
				.WithMany(t => t.Cards)
				.HasForeignKey(tc => tc.TableId);

			modelBuilder.Entity<TableCard>()
				.HasOne<Card>(tc => tc.Card)
				.WithMany(t => t.Tables)
				.HasForeignKey(tc => tc.CardId);

			//Deck cards relations
			modelBuilder.Entity<DeckCard>()
				.HasOne<PokerTable>(tc => tc.PokerTable)
				.WithMany(t => t.CardDeck)
				.HasForeignKey(tc => tc.TableId);

			modelBuilder.Entity<DeckCard>()
				.HasOne<Card>(tc => tc.Card)
				.WithMany(t => t.Decks)
				.HasForeignKey(tc => tc.CardId);

			//Player Card relations
			modelBuilder.Entity<PlayerCard>()
				.HasOne<Player>(pc => pc.Player)
				.WithMany(p => p.Cards)
				.HasForeignKey(pc => pc.PlayerId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<PlayerCard>()
				.HasOne<Card>(pc => pc.Card)
				.WithMany(p => p.Players)
				.HasForeignKey(pc => pc.CardId)
				.OnDelete(DeleteBehavior.NoAction);

		}

		public DbSet<GameEngine.Models.Game.User> User { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Player> Player { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.PokerTable> Table { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Accessory> Accessory { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Card> Card { get; set; } = default!;
		public DbSet<PlayerCard> PlayerCards { get; set; } = default!;
		public DbSet<TableCard> TableCards { get; set; } = default!;
		public DbSet<DeckCard> DeckCards { get; set; } = default!;
	}

}
