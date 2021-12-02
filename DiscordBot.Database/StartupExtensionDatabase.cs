using DiscordBot.Database.Interfaces;
using DiscordBot.Database.RandomGame;
using DiscordBot.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Database
{
    public static class StartupExtensionDatabase
    {
        public static void AddDatabases(this IServiceCollection services)
        {
            AddRandomGameContext(services);
        }

        private static void AddRandomGameContext(IServiceCollection services)
        {
            new RandomGameContext().Database.Migrate();
            services.AddDbContext<RandomGameContext>();

            services.AddScoped<IRandomGameService, RandomGameService>();
            services.AddScoped<IRandomGameScoreService, RandomGameScoreService>();
        }
    }
}