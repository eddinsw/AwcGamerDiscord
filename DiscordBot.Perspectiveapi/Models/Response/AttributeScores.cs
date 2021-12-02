namespace DiscordBot.Perspectiveapi.Models.Response
{
    internal class AttributeScores
    {
        public INSULT INSULT { get; set; }
        public THREAT THREAT { get; set; }
        public PROFANITY PROFANITY { get; set; }
        public TOXICITY TOXICITY { get; set; }
    }
}