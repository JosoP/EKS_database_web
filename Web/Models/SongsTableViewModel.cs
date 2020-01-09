using System.Collections.Generic;
using Database.Models.Songs;

namespace Web.Models
{
    /// <summary>
    ///     Model used for song table partial view.
    /// </summary>
    public class SongsTableViewModel
    {
        /// <summary>
        ///     List of songs to be displayed in song table.
        /// </summary>
        public List<Song> Songs { get; set; }
        
        /// <summary>
        ///     List of all categories used for filtering of songs.
        /// </summary>
        public List<Category> Categories { get; set; }
        
        /// <summary>
        ///     Mode in which song table is running.
        /// </summary>
        public Mode ViewMode { get; set; }

        public enum Mode
        {
            Maintaining, // table of songs with possibility to make CRUD operations
            Selecting, // table of songs with possibility of select song
        }
    }
}