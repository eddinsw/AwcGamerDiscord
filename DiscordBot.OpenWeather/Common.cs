using DiscordBot.Configuration;

namespace DiscordBot.OpenWeather
{
    internal static class Common
    {
        public static string ZipUrl(string zip) => @$"http://api.openweathermap.org/data/2.5/weather?zip={zip}&units=imperial&appid={DiscordBotConfiguration.OpenWeatherApiKey}";

        public static string IconUrl(string icon) => @$"https://openweathermap.org/img/w/{icon}.png";

        public static string IconUrl_2x(string icon) => @$"https://openweathermap.org/img/wn/{icon}@2x.png";
    }
}