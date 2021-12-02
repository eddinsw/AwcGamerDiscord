using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBot.Common.QuotesList
{
    public static class DadJokes
    {
        private const int MaxRetries = 3;
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        public static string RandomMessage()
        {
            var message = GetRandom();
            message.Wait();
            return message.Result;
        }

        public static async Task<string> GetRandom()
        {
            HttpResponseMessage httpResponse;
            var tries = 0;

            do
            {
                tries++;

                if (!_httpClient.DefaultRequestHeaders.Any()) _httpClient.DefaultRequestHeaders.Add("Accept", "text/plain");

                httpResponse = await _httpClient.GetAsync(@"https://icanhazdadjoke.com/");
                if (tries == MaxRetries) httpResponse.EnsureSuccessStatusCode();
                if (!httpResponse.IsSuccessStatusCode) continue;

                await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(responseStream);
                var response = await streamReader.ReadToEndAsync();

                if (string.IsNullOrWhiteSpace(response)) continue;

                return response;
            } while (!httpResponse.IsSuccessStatusCode && tries <= MaxRetries);
            throw new Exception($"Something went wrong but no Exception was caught. Return Message Contents : {await httpResponse.Content.ReadAsStringAsync()}");
        }
    }
}