using Discord.Commands;
using DiscordBot.Database.Interfaces;

namespace DiscordBot.Commands
{
    public class SimpleGameModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        private readonly IRandomGameService _gameService;
        private readonly IRandomGameScoreService _gameScoreService;

        public SimpleGameModule(CommandService service, IRandomGameService gameService, IRandomGameScoreService gameScoreService)
        {
            _service = service;
            _gameService = gameService;
            _gameScoreService = gameScoreService;
        }

        //[RequireUserPermission(GuildPermission.Administrator)]
        //[RequireContext(ContextType.Guild)]
        //[Command("game")]
        //public Task CreateGame()
        //{
        //    return;
        //}
        // Create Game

        // Get Top 5

        // Get New Score
    }
}