using Microsoft.Extensions.Configuration;

namespace DiscordBot.Configuration
{
    public static class DiscordBotConfiguration
    {
        public static string Token => GetEnvironmentSetting("Discord:Token");
        public static string CommandPrefix => GetEnvironmentSetting("Discord:CommandPreFix");

        public static string OpenWeatherApiKey => GetEnvironmentSetting("OpenWeather:ApiKey");
        public static string PerspectiveToken => GetEnvironmentSetting("Perspective:Token");
        public static string UnsplashAccessToken => GetEnvironmentSetting("Unsplash:AccessToken");

        private static readonly IConfigurationRoot ConfigBuilder;
        private const string APPENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        private static readonly string Environment = System.Environment.GetEnvironmentVariable(APPENVIRONMENT);

        static DiscordBotConfiguration()
        {
            ConfigBuilder = new ConfigurationBuilder()
                .AddJsonFile("app.environment.json", optional: false)
                .AddJsonFile($"app.environment.{Environment}.json", optional: true)
                .Build();
        }

        private static string GetEnvironmentSetting(string key) => ConfigBuilder[key];
    }
}