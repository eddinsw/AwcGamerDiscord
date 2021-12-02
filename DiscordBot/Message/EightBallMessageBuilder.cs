using Discord;
using DiscordBot.Common.QuotesList;

namespace DiscordBot.Message
{
    public static class EightBallMessageBuilder
    {
        public static Embed Message(string msg)
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(255, 255, 0),
                Description = msg
            };

            builder.AddField(x =>
            {
                x.Name = "Answer";
                x.Value = EightBallAnswers.RandomMessage;
                x.IsInline = false;
            });

            return builder.Build();
        }
    }
}