using Discord.Commands;
using DiscordBot.Common.QuotesList;
using DiscordBot.Configuration;
using DiscordBot.Message;
using DiscordBot.Perspectiveapi;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IServiceProvider _provider;
        private readonly IPerspectiveCalls _perspectiveCalls;

        public CommandHandler(
            DiscordSocketClient discord,
            CommandService commands,
            IServiceProvider provider,
            IPerspectiveCalls perspectiveCalls)
        {
            _discord = discord;
            _commands = commands;
            _provider = provider;
            _perspectiveCalls = perspectiveCalls;

            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return; // Is a message?
            if (msg.Author.Id == _discord.CurrentUser.Id) return; // Ignore self

            var context = new SocketCommandContext(_discord, msg); // Create the command context

            var user = "User";
            if (msg.Author is SocketGuildUser author)
            {
                user = string.IsNullOrWhiteSpace(author.Nickname) ? author.Username : author.Nickname;
            }

            var argPos = 0;     // Check if the message has a valid command prefix
            if (msg.HasStringPrefix(DiscordBotConfiguration.CommandPrefix, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _provider);
                if (!result.IsSuccess) await context.Channel.SendMessageAsync($"{user}, I got this error : {result.ErrorReason}");
            }
            else if (msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                // @ bot in channel - Insult user
                await context.Channel.SendMessageAsync($"{user}, {Insults.RandomMessage}");
            }
            else
            {
                var toxic = await _perspectiveCalls.GetToxic(msg.Content);
                if (ToxicMessageBuilder.IsToxic(toxic))
                {
                    await context.Channel.SendMessageAsync($"{user}", false, ToxicMessageBuilder.Message(msg.Content, toxic));
                }
            }
        }
    }
}