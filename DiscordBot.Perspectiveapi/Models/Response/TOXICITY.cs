using System.Collections.Generic;

namespace DiscordBot.Perspectiveapi.Models.Response
{
    internal class TOXICITY
    {
        public List<SpanScore> spanScores { get; set; }
        public SummaryScore summaryScore { get; set; }
    }
}