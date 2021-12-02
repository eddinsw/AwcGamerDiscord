using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("say")]
    public class SayModule : ModuleBase<SocketCommandContext>
    {
        [Name("say")]
        [Group("say"), Alias("s")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public class SayBase : ModuleBase
        {
            [Command, Priority(0)]
            [Summary("Make the bot say something")]
            public Task SayText([Remainder] string text)
                => ReplyAsync(text);

            [Command("all"), Priority(1)]
            [Summary("Make the bot say something to all")]
            public Task SayAllText([Remainder] string text)
                => ReplyAsync($"@everyone {text}");

            [Command("here"), Priority(1)]
            [Summary("Make the bot say something and alert online people")]
            public Task SayHereText([Remainder] string text)
                => ReplyAsync($"@here {text}");
        }
    }
}