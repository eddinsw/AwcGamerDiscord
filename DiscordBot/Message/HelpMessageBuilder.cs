using Discord;
using Discord.Commands;
using DiscordBot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Message
{
    internal static class HelpMessageBuilder
    {
        public static Embed GeneralHelp => GeneralHelpEmbed();

        private static Embed GeneralHelpEmbed()
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(255, 255, 0),
                Title = "Help",
                Description = $"Type **{DiscordBotConfiguration.CommandPrefix}help all** to see all commands{Environment.NewLine}If you @me, I will insult you."
            };

            builder.AddField(x =>
            {
                x.Name = "Roll Dice";
                x.Value = $"Command: **{DiscordBotConfiguration.CommandPrefix}roll**{Environment.NewLine}Output: {{user}}rolled 1 * D6 - {{ 3 }}{Environment.NewLine}Command: **{DiscordBotConfiguration.CommandPrefix}roll d6 2**{Environment.NewLine}Output:{{user}} rolled 2 * D6 {{ 2 | 4 }}";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Random Quote";
                x.Value = $"Command: **{DiscordBotConfiguration.CommandPrefix}quote**";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Random Beer Quote";
                x.Value = $"Command: **{DiscordBotConfiguration.CommandPrefix}beer**";
                x.IsInline = false;
            });

            return builder.Build();
        }

        public static async Task<Embed> AllHelpEmbed(IEnumerable<ModuleInfo> modules, SocketCommandContext context)
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(255, 255, 0),
                Title = "Help",
                Description = "These are the commands you can use"
            };

            foreach (var module in modules)
            {
                if (module.Name == "HelpModule") continue;
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(context);
                    if (result.IsSuccess)
                        description += $"{DiscordBotConfiguration.CommandPrefix}{cmd.Aliases.First()}\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            return builder.Build();
        }

        public static Embed HelpEmbed(string command, IReadOnlyList<CommandMatch> commands)
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(255, 255, 0),
                Title = "Help",
                Description = $"Here are some commands like **{command}**"
            };

            foreach (var match in commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}{Environment.NewLine}Summary: {cmd.Summary}";
                    x.IsInline = false;
                });
            }

            return builder.Build();
        }
    }
}