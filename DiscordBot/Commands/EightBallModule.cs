using Discord.Commands;
using DiscordBot.Message;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("8ball")]
    public class EightBallModule : ModuleBase<SocketCommandContext>
    {
        [Command("8ball"), Alias("8B")]
        [Summary("MAGIC 8 BALL : Get answers with Magic eight ball")]
        public async Task HelpAsync([Remainder] string msg = "")
        {
            if (!msg.EndsWith("?"))
            {
                await YouShouldAskAQuestion();
                return;
            }
            await ReplyAsync("", false, EightBallMessageBuilder.Message(msg));
        }

        private Task YouShouldAskAQuestion() => ReplyAsync("You should ask a Question");
    }
}