using System.Collections.Generic;
using Database.Models.Songs;

namespace Web.Models
{
    public class SongsTableViewModel
    {
        public List<Song> Songs { get; set; }
        public List<Category> Categories { get; set; }
        public Mode ViewMode { get; set; }

        public enum Mode
        {
            Maintaining, // table of songs with possibility to make CRUD operations
            Selecting, // table of songs with possibility of select song
        }
    }
}