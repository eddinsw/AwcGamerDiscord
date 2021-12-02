using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("tacos")]
    public class TacoModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;

        public TacoModule(CommandService service)
        {
            _service = service;
        }

        [RequireContext(ContextType.Guild)]
        [Command("tacos")]
        [Summary("EAT TACOS")]
        public async Task TacosAsync()
        {
            if (!(Context.User is SocketGuildUser author)) return;
            var messageName = string.IsNullOrWhiteSpace(author.Nickname) ? author.Username : author.Nickname;
            var numberOfTacos = new Random().Next(12) + 1;
            await ReplyAsync(numberOfTacos == 1 ? $"{messageName} ate {numberOfTacos} Taco." : $"{messageName} ate {numberOfTacos} Tacos.");
        }
    }
}