using System;
using System.Collections.Generic;
using System.Text;

namespace MetaImporter
{
    public sealed record GameRecord
    {
        // From CSV
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public double MyRating { get; set; }
        public string DateCompleted { get; set; } = string.Empty;
        public string System { get; set; } = string.Empty;

        // From SteamGrid
        public Uri? ImageURI { get; set; }

        //From RAWG
        public RawgGame? RawgInfo { get; set; }
    }
}
