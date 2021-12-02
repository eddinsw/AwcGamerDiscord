using System.Collections.Generic;

namespace DiscordBot.Perspectiveapi.Models.Response
{
    internal class BaseResponse
    {
        public AttributeScores attributeScores { get; set; }
        public List<string> languages { get; set; }
        public List<string> detectedLanguages { get; set; }
    }
}