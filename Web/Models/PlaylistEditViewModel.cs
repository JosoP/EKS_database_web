using System.Collections.Generic;
using Database.Models.Songs;

namespace Web.Models
{
    public class PlaylistEditViewModel
    {
        public Playlist Playlist { get; set; }
        public SongsTableViewModel SongsTableViewModel { get; set; }
        public List<Song> SelectedSongs { get; set; }
    }
}