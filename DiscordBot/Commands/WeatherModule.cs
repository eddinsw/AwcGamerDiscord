using Discord;
using Discord.Commands;
using DiscordBot.OpenWeather;
using DiscordBot.OpenWeather.Models;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    [Name("weather")]
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        [Command("weather")]
        [Summary("weather for zip code")]
        public Task DisplayWeather(string zip)
        {
            try
            {
                var weather = WeatherNow.Get(zip);
                return EmbedMessage(weather);
            }
            catch
            {
                return ReplyAsync($"Zip {zip} not Found");
            }
        }

        [Command("weather")]
        [Summary("weather for Winchester, US")]
        public Task DisplayWeather()
        {
            return DisplayWeather("22602");
        }

        private Task EmbedMessage(WeatherNowOutput now)
        {
            var newMessage = new EmbedBuilder
            {
                Title = $"{now.Title} - {now.Description}",
                ThumbnailUrl = now.Thumbnail,
                Description = $"{now.City}"
            };

            return ReplyAsync("", false, newMessage.Build());
        }
    }
}