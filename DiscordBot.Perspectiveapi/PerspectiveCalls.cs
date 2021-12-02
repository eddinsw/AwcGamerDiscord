using DiscordBot.Perspectiveapi.Models.Request;
using DiscordBot.Perspectiveapi.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Perspectiveapi
{
    public interface IPerspectiveCalls
    {
        Task<Dictionary<string, string>> GetToxic(string msg);
    }

    public class PerspectiveCalls : IPerspectiveCalls
    {
        private const int MaxRetries = 3;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializer _jsonSerializer;

        public PerspectiveCalls(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializer = new JsonSerializer();
        }

        public async Task<Dictionary<string, string>> GetToxic(string msg)
        {
            HttpResponseMessage httpResponse;
            var tries = 0;

            var json = JsonConvert.SerializeObject(new BaseRequest(msg));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            do
            {
                tries++;
                httpResponse = await _httpClient.PostAsync(PerspectiveSettings.FullApiUri, httpContent);
                if (tries == MaxRetries) httpResponse.EnsureSuccessStatusCode();
                if (!httpResponse.IsSuccessStatusCode) continue;

                await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(responseStream);
                using var jsonTextReader = new JsonTextReader(streamReader);
                var response = _jsonSerializer.Deserialize<BaseResponse>(jsonTextReader);

                if (response == null) continue;

                var rtnSet = new Dictionary<string, string>
                {
                    {"TOXICITY", response.attributeScores.TOXICITY.summaryScore.value.ToString("P2")},
                    {"PROFANITY", response.attributeScores.PROFANITY.summaryScore.value.ToString("P2")},
                    {"THREAT", response.attributeScores.THREAT.summaryScore.value.ToString("P2")},
                    {"INSULT", response.attributeScores.INSULT.summaryScore.value.ToString("P2")}
                };

                return rtnSet;
            } while (!httpResponse.IsSuccessStatusCode && tries <= MaxRetries);
            throw new Exception($"Something went wrong but no Exception was caught. Return Message Contents : {await httpResponse.Content.ReadAsStringAsync()}");
        }
    }
}