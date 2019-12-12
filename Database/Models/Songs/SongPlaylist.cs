using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.Songs
{
    [Table("song_playlist")]
    public partial class SongPlaylist
    {
        [Column("song")]
        public long SongId { get; set; }
        
        [Column("playlist")]
        public long PlaylistId { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        public virtual Playlist Playlist { get; set; }
        
        [ForeignKey(nameof(SongId))]
        public virtual Song Song { get; set; }
    }
}
