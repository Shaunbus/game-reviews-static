using System;
using System.Collections.Generic;
using System.Text;

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

            List<ReviewRecord> test = reader.LoadRecords($"{twoFoldersUp}/_data/reviews.csv");
        }
    }
}
