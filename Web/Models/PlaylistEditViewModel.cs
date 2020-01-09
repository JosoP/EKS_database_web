using System.Collections.Generic;
using Database.Models.Songs;

namespace Web.Models
{
    /// <summary>
    ///     Model used for playlist edit and create View.
    /// </summary>
    public class PlaylistEditViewModel
    {
        /// <summary>
        ///     Currently editing or creating playlist.
        /// </summary>
        public Playlist Playlist { get; set; }
        
        /// <summary>
        ///     View model for table of songs that contains song that are not currently selected for playlist.
        /// </summary>
        public SongsTableViewModel SongsTableViewModel { get; set; }
        
        /// <summary>
        ///     List of songs that are currently selected for playlist.
        /// </summary>
        public List<Song> SelectedSongs { get; set; }
    }
}