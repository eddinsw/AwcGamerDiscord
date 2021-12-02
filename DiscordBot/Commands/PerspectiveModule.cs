using Discord.Commands;
using DiscordBot.Message;
using DiscordBot.Perspectiveapi;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("perspective")]
    public class PerspectiveModule : ModuleBase<SocketCommandContext>
    {
        private readonly IPerspectiveCalls _perspectiveCalls;

        public PerspectiveModule(IPerspectiveCalls perspectiveCalls)
        {
            _perspectiveCalls = perspectiveCalls;
        }

        [Command("toxic"), Priority(0)]
        [Summary("Get Toxic Rating from Perspective Api")]
        public async Task ToxicAsync([Remainder] string msg)
        {
            var iAmToxic = await _perspectiveCalls.GetToxic(msg);
            await ReplyAsync("", false, ToxicMessageBuilder.Message(msg, iAmToxic));
        }
    }
}