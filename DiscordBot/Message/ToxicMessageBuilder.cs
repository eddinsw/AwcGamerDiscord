using Discord;
using System.Collections.Generic;

namespace DiscordBot.Message
{
    public static class ToxicMessageBuilder
    {
        public static bool IsToxic(Dictionary<string, string> iAmToxic)
        {
            const double mintoxic = 75.0;
            var toxic = double.Parse(iAmToxic.GetValueOrDefault("TOXICITY").Replace("%", ""));
            var profanity = double.Parse(iAmToxic.GetValueOrDefault("PROFANITY").Replace("%", ""));
            var threat = double.Parse(iAmToxic.GetValueOrDefault("THREAT").Replace("%", ""));
            var insult = double.Parse(iAmToxic.GetValueOrDefault("INSULT").Replace("%", ""));
            return toxic >= mintoxic || profanity >= mintoxic || threat >= mintoxic || insult >= mintoxic;
        }

        public static Embed Message(string msg, Dictionary<string, string> iAmToxic)
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(255, 255, 0),
                Description = msg
            };

            builder.AddField(x =>
            {
                x.Name = "TOXICITY";
                x.Value = iAmToxic.GetValueOrDefault("TOXICITY");
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "PROFANITY";
                x.Value = iAmToxic.GetValueOrDefault("PROFANITY");
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "THREAT";
                x.Value = iAmToxic.GetValueOrDefault("THREAT");
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "INSULT";
                x.Value = iAmToxic.GetValueOrDefault("INSULT");
                x.IsInline = false;
            });

            return builder.Build();
        }
    }
}