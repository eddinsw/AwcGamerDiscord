using DiscordBot.Database.RandomGame.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DiscordBot.Database.RandomGame
{
    public class RandomGameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameScore> GameScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($@"Data Source={Path.Combine(Common.Constants.ROOTDIR, "RandomGame.db")}");
    }
}