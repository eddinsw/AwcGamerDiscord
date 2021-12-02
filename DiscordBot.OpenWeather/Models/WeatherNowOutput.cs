using System.Globalization;

namespace DiscordBot.OpenWeather.Models
{
    public class WeatherNowOutput
    {
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string City { get; set; }
        public string Description { get; set; }

        private char Fahrenheit = '℉';

        internal WeatherNowOutput(NowWeatherResponse resp, string zip)
        {
            Title = $"{resp.main.temp:0.0}{Fahrenheit}";
            Thumbnail = Common.IconUrl_2x(resp.weather[0].icon);
            City = $"{resp.name}, {resp.sys.country} ({zip})";
            Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(resp.weather[0].description);
        }
    }
}