using DiscordBot.Configuration;

namespace DiscordBot.Perspectiveapi
{
    internal static class PerspectiveSettings
    {
        public static string ApiUri = @"https://commentanalyzer.googleapis.com/v1alpha1/comments:analyze";
        public static string FullApiUri = $"{ApiUri}?key={DiscordBotConfiguration.PerspectiveToken}";
    }
}