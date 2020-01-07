using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [DisplayName("Playlist")]
        public virtual Playlist Playlist { get; set; }
        
        [JsonIgnore]
        [ForeignKey(nameof(SongId))]
        [DisplayName("Pieseň")]
        public virtual Song Song { get; set; }
    }
}
