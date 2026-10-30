using System;
using System.Collections.Generic;
using System.Text;

namespace MetaImporter
{
    public sealed record GameRecord
    {
        public string Name { get; set; } = string.Empty;
        public double MyRating { get; set; }
        public string DateCompleted { get; set; } = string.Empty;
        public string System { get; set; } = string.Empty;
        public Uri? ImageURI { get; set; }
        public string MetaRating { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string RawgId { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}
