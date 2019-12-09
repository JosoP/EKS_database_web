using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKS_database_web.Data
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
