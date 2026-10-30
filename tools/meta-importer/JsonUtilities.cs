using System.Text.Json;

namespace MetaImporter
{
    internal class JsonUtilities
    {
        public void WriteToFile(object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);
            string filePath = "reviews.json";
            File.WriteAllText(filePath, jsonString);
            System.Console.WriteLine($"Writing to {filePath}!");
        }
    }
}
