using DiscordBot.OpenWeather.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBot.OpenWeather
{
    public static class WeatherNow
    {
        private const int MaxRetries = 3;
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        public static WeatherNowOutput Get(string zip)
        {
            var newWeather = GetWeather(zip);
            newWeather.Wait();
            return new WeatherNowOutput(newWeather.Result, zip);
        }

        private static async Task<NowWeatherResponse> GetWeather(string zip)
        {
            HttpResponseMessage httpResponse;
            var tries = 0;

            do
            {
                tries++;

                httpResponse = await _httpClient.GetAsync(Common.ZipUrl(zip));
                if (tries == MaxRetries) httpResponse.EnsureSuccessStatusCode();
                if (!httpResponse.IsSuccessStatusCode) continue;

                await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(responseStream);
                using var jsonTextReader = new JsonTextReader(streamReader);
                var response = _jsonSerializer.Deserialize<NowWeatherResponse>(jsonTextReader);

                if (response == null) continue;

                return response;
            } while (!httpResponse.IsSuccessStatusCode && tries <= MaxRetries);
            throw new Exception($"Something went wrong but no Exception was caught. Return Message Contents : {await httpResponse.Content.ReadAsStringAsync()}");
        }
    }
}