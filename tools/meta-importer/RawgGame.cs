namespace MetaImporter
{
    public sealed class RawgGame
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Released { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int? Metacritic { get; set; }

        public List<RawgGenre> Genres { get; set; } = [];

        public float Rating { get; set; }
        //public string esrb_rating { get; set; } = string.Empty;

    }
}
