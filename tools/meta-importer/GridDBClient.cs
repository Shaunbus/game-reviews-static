using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MetaImporter
{
    internal class GridDBClient
    {
        private readonly HttpClient _httpClient = new();

        public string SearchGame(string name, string apiKey)
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://www.steamgriddb.com/api/v2/search/autocomplete/{Uri.EscapeDataString(name)}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = _httpClient.Send(request);

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public int? GetGameId(string gameName, string apiKey)
        {
            var url = $"https://www.steamgriddb.com/api/v2/search/autocomplete/{Uri.EscapeDataString(gameName)}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var result = _httpClient.GetFromJsonAsync<SteamGridSearchResponse>(url).GetAwaiter().GetResult();
            var gameId = result!.Data.First().Id;
            return gameId;
        }

        public SteamGridGrid GetGrids(int? id, string apiKey)
        {
            var url = $"https://www.steamgriddb.com/api/v2/grids/game/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var result = _httpClient.GetFromJsonAsync<SteamGridGridResponse>(url).GetAwaiter().GetResult();
            if(result.Data.Count == 0)
            {
                Console.WriteLine("Couldn't Find Game");
                return null;
            }
            return result.Data.OrderByDescending(x => x.Score).First();
        }

        /// <summary>
        /// Using a URL downloads the image to the asset folder
        /// </summary>
        /// <param name="url"></param>
        /// <param name="slug"></param>
        /// <exception cref="Exception"></exception>
        public void DownloadCover(string url, string slug)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("Unauthorized on request. Server says: " + response.Headers.WwwAuthenticate.ToString());
            }

            var bytes = _httpClient.GetByteArrayAsync(url).GetAwaiter().GetResult();
            Console.WriteLine(Path.Combine("assets", "images", $"{slug}.png"), bytes);
            File.WriteAllBytes(Path.Combine("assets", "images", $"{slug}.png"), bytes);
        }
    }

    public sealed class SteamGridSearchResponse
    {
        public List<SteamGridGame> Data { get; set; } = new();
    }

    public sealed class SteamGridGame
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public sealed class SteamGridGridResponse
    {
        public List<SteamGridGrid> Data { get; set; }
            = new();
    }

    public sealed class SteamGridGrid
    {
        public int Score { get; set; }

        public string Url { get; set; }
            = string.Empty;
    }
}
