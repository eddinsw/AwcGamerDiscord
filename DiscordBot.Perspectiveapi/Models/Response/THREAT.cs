using System.Collections.Generic;

namespace DiscordBot.Perspectiveapi.Models.Response
{
    internal class THREAT
    {
        public List<SpanScore> spanScores { get; set; }
        public SummaryScore summaryScore { get; set; }
    }
}