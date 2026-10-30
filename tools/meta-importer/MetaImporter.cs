using Slugify;

namespace MetaImporter
{
    public class MetaDataImporter
    {
        public void Run()
        {
            DotNetEnv.Env.TraversePath().Load();
            //string? apiKey = Environment.GetEnvironmentVariable("API_KEY");
            string? apiKey = Environment.GetEnvironmentVariable("GRID_KEY");

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
            jsonTool.WriteToFile(games);
            foreach (GameRecord game in games)
            {
                //check if cover art already exists
                if (!File.Exists(Path.Combine("assets", "images", $"{game.Slug}.png")))
                    {
                    Console.WriteLine("Searching for: " + game.Name);
                    int? id = gridClient.GetGameId(game.Name, apiKey);
                    // Get Top Rated Grid
                    
                    var coverArt = gridClient.GetGrids(id, apiKey);

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
                //var result = rawgClient.GetGameSearch(game.Slug, apiKey);
                //var gameResult = result.Result.Results.First();
            }
            
        }
    }
}
