using System.Collections.Generic;

namespace DiscordBot.Perspectiveapi.Models.Response
{
    internal class INSULT
    {
        public List<SpanScore> spanScores { get; set; }
        public SummaryScore summaryScore { get; set; }
    }
}