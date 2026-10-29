namespace MetaImporter
{
    public record ReviewRecord
    {
        public string name { get; set; } = string.Empty;
        public int rating { get; set; }
        public string date { get; set; } = string.Empty;
        public string system { get; set; } = string.Empty;
    }
}
