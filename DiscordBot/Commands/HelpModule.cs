using Discord.Commands;
using DiscordBot.Message;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;

        public HelpModule(CommandService service)
        {
            _service = service;
        }

        [Command("help"), Priority(0)]
        public async Task HelpAsync() => await ReplyAsync("", false, HelpMessageBuilder.GeneralHelp);

        [Command("help all"), Priority(2)]
        public async Task HelpAllAsync() => await ReplyAsync("", false, await HelpMessageBuilder.AllHelpEmbed(_service.Modules, Context));

        [Command("help"), Priority(1)]
        public async Task HelpAsync([Remainder] string command)
        {
            var result = _service.Search(Context, command);

            if (!result.IsSuccess) await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
            else await ReplyAsync("", false, HelpMessageBuilder.HelpEmbed(command, result.Commands));
        }
    }
}