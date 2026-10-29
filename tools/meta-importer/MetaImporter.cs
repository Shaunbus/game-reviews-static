using System;
using System.Collections.Generic;
using System.Text;
using Slugify;

namespace MetaImporter
{
    public class MetaDataImporter
    {
        public void Run()
        {
            DotNetEnv.Env.TraversePath().Load();
            string? apiKey = Environment.GetEnvironmentVariable("API_KEY");

            Console.WriteLine($"Your API Key is: {apiKey}");

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string twoFoldersUp = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", ".."));

            // Import csv
            var reader = new CsvUtilities();
            var helper = new SlugHelper();

            List<ReviewRecord> reviews = reader.LoadRecords($"{twoFoldersUp}/_data/reviews.csv");
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
            foreach (GameRecord game in games)
            {  
                //var result = await rawgClient.GetGameFromSlug(game.Slug, apiKey);
                var result = rawgClient.GetGameAsync(game.Slug, apiKey);
                //var test = result.Results;
                //Console.WriteLine(result?.Results.Count);
            }       
        }
    }
}
