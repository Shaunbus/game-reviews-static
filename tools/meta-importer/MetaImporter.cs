using Slugify;
using System.Text.Json;

namespace MetaImporter
{
    public class MetaDataImporter
    {
        public void Run()
        {
            // Load the API Keys
            DotNetEnv.Env.TraversePath().Load();
            string? rawgApiKey = Environment.GetEnvironmentVariable("RAWG_KEY");
            string? gridApiKey = Environment.GetEnvironmentVariable("GRID_KEY");

            // TODO Move this to a different location
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string twoFoldersUp = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));

            // Import csv
            var reader = new CsvUtilities();
            var helper = new SlugHelper();

            List<ReviewRecord> reviews = reader.LoadRecords($"{twoFoldersUp}/Data/reviews.csv");

            List<GameRecord> games = reviews.Select(review => new GameRecord
            {
                Name = review.name,
                MyRating = review.rating,
                DateCompleted = review.date,
                System = review.system,
                Slug = helper.GenerateSlug(review.name)
            }
            ).ToList();

            var rawgClient = new RawgClient();
            var gridClient = new GridDBClient();
            var imageClient = new GridDBClient();
            var jsonTool = new JsonUtilities();

            var root =
                    Path.GetFullPath(
                        Path.Combine(
                            AppContext.BaseDirectory,
                            "..",
                            "..",
                            "..",
                            "..",
                            ".."));

            var reviewsPath =
                Path.Combine(
                    root,
                    "_data",
                    "reviews.json");

            var json =  File.ReadAllText(reviewsPath);

            var gamesCache = JsonSerializer.Deserialize<List<GameRecord>>(json) ?? [];

            foreach (GameRecord game in games)
            {
                //check if cover art already exists
                if (!File.Exists(Path.Combine("assets", "images", $"{game.Slug}.png")))
                {
                    Console.WriteLine("Searching for: " + game.Name);
                    int? id = gridClient.GetGameId(game.Name, gridApiKey);
                    // Get Top Rated Grid
                    
                    var coverArt = gridClient.GetGrids(id, gridApiKey);

                    if (coverArt != null)
                    {
                        Console.WriteLine(coverArt.Url);
                        // Download it locally
                        imageClient.DownloadCover(coverArt.Url, game.Slug);
                    }
                }
                else
                {
                    Console.WriteLine(game.Name + " Artwork Already Exists!");
                }

                // Populate it with Metadata
                // TODO: Make this better, iterate through the search and pick the correct game
                var existing = gamesCache.FirstOrDefault(x => x.Name == game.Name);
                if (existing is not null)
                {
                    // Right now I'm going to manually fix IDs and refresh the metadata here
                    var result = rawgClient.GetGame(existing.RawgInfo.Id, rawgApiKey);
                    game.RawgInfo = result.Result;
                }
                else
                {       
                    // New entries use search which sucks
                    var result = rawgClient.GetGameSearch(game.Slug, rawgApiKey);
                    RawgGame gameResult = result.Result.Results.First();
                    Console.WriteLine($"Searched {game.Slug}. Returned {gameResult.Slug}");
                    game.RawgInfo = gameResult;
                }
            }

            jsonTool.WriteToFile(games);

        }
    }
}
