using DiscordBot.Common.Image.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBot.Common.Image
{
    internal class FoxImage
    {
        private const int MaxRetries = 3;
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        public static string Image()
        {
            var newDog = GetRandom();
            newDog.Wait();
            return newDog.Result;
        }

        private static async Task<string> GetRandom()
        {
            HttpResponseMessage httpResponse;
            var tries = 0;

            do
            {
                tries++;

                httpResponse = await _httpClient.GetAsync(@"https://randomfox.ca/floof/");
                if (tries == MaxRetries) httpResponse.EnsureSuccessStatusCode();
                if (!httpResponse.IsSuccessStatusCode) continue;

                await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(responseStream);
                using var jsonTextReader = new JsonTextReader(streamReader);
                var response = _jsonSerializer.Deserialize<FoxImageResponse>(jsonTextReader);

                if (response == null) continue;

                return response.image;
            } while (!httpResponse.IsSuccessStatusCode && tries <= MaxRetries);
            throw new Exception($"Something went wrong but no Exception was caught. Return Message Contents : {await httpResponse.Content.ReadAsStringAsync()}");
        }
    }
}