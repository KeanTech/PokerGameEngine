using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameEngine.Models.Game;

namespace GameEngine.Data
{
    public class GameEngineContext : DbContext
    {
        public GameEngineContext (DbContextOptions<GameEngineContext> options)
            : base(options)
        {
        }

        public DbSet<GameEngine.Models.Game.User> User { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Table> Table { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Accessory> Accessory { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Chip> Chip { get; set; } = default!;
		public DbSet<GameEngine.Models.Game.Card> Card { get; set; } = default!;
	}
}
