using System.Net.Http;
using System.Net.Http.Json;

namespace MetaImporter
{
    internal class RawgClient
    {
        private readonly HttpClient _httpClient = new();

        public async Task<RawgSearchResponse?> GetGameAsync(string slug, string apiKey)
        {
            return _httpClient.GetFromJsonAsync<RawgSearchResponse>($"https://api.rawg.io/api/games?search={slug}&key={apiKey}").GetAwaiter().GetResult(); ;
        }

        public async Task<string> GetGameFromSlug(string slug, string apiKey)
        {
            string url = $"https://api.rawg.io/api/games?search={slug}&key={apiKey}";

            return await _httpClient.GetStringAsync(url);
        }

    }
}
