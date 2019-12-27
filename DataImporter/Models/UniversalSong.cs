using System;
using System.Collections.Generic;

namespace DataImporter.Models
{
    public class UniversalSong
    {
        public string Title { get; set; } = null;

        public int? Number { get; set; } = null;

        public string Authors { get; set; } = null;

        public string Url { get; set; } = null;

        public List<UniversalVerse> Verses { get; set; } = null;

        public List<UniversalCategory> Categories { get; set; } = null;
        
        public List<UniversalPlaylist> Playlists { get; set; } = null;

        public DateTime CreationDate { get; set; }        
        
    }
}