using DiscordBot.Common.Image.Model;
using DiscordBot.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBot.Common.Image
{
    internal class UnsplashImage
    {
        private const int MaxRetries = 3;
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        private const string LANDSCAPE = "landscape";
        private const string PORTRAIT = "portrait";

        public static string Image()
        {
            var randomNew = GetRandom();
            randomNew.Wait();
            return randomNew.Result;
        }

        public static Dictionary<string, string> Background(bool portrait = false)
        {
            var randomNew = GetRandomBackGround(portrait);
            randomNew.Wait();
            return randomNew.Result;
        }

        private static async Task<Dictionary<string, string>> GetRandomBackGround(bool portrait = false)
        {
            HttpResponseMessage httpResponse;
            var tries = 0;

            do
            {
                tries++;
                var orientation = portrait ? PORTRAIT : LANDSCAPE;

                httpResponse = await _httpClient.GetAsync($@"https://api.unsplash.com/photos/random/?client_id={DiscordBotConfiguration.UnsplashAccessToken}&collections=8961198&orientation={orientation}");
                if (tries == MaxRetries) httpResponse.EnsureSuccessStatusCode();
                if (!httpResponse.IsSuccessStatusCode) continue;

                await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(responseStream);
                using var jsonTextReader = new JsonTextReader(streamReader);
                var response = _jsonSerializer.Deserialize<UnsplashImageResponse>(jsonTextReader);

                if (response == null) continue;

                var backGround = new Dictionary<string, string>
                {
                    {"small", response.urls.small},
                    {"raw", response.urls.raw},
                    {"fullSize", response.urls.full}
                };

                return backGround;
            } while (!httpResponse.IsSuccessStatusCode && tries <= MaxRetries);
            throw new Exception($"Something went wrong but no Exception was caught. Return Message Contents : {await httpResponse.Content.ReadAsStringAsync()}");
        }

        private static async Task<string> GetRandom()
        {
            HttpResponseMessage httpResponse;
            var tries = 0;

            do
            {
                tries++;

                httpResponse = await _httpClient.GetAsync(@$"https://api.unsplash.com/photos/random/?client_id={DiscordBotConfiguration.UnsplashAccessToken}");
                if (tries == MaxRetries) httpResponse.EnsureSuccessStatusCode();
                if (!httpResponse.IsSuccessStatusCode) continue;

                await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(responseStream);
                using var jsonTextReader = new JsonTextReader(streamReader);
                var response = _jsonSerializer.Deserialize<UnsplashImageResponse>(jsonTextReader);

                if (response == null) continue;

                return response.urls.small;
            } while (!httpResponse.IsSuccessStatusCode && tries <= MaxRetries);
            throw new Exception($"Something went wrong but no Exception was caught. Return Message Contents : {await httpResponse.Content.ReadAsStringAsync()}");
        }
    }
}