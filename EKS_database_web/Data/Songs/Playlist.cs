using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKS_database_web.Data
{
    [Table("playlists")]
    public partial class Playlist
    {
        public Playlist()
        {
            SongPlaylists = new HashSet<SongPlaylist>();
        }

        [Key]
        [Column("_id")]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("lastModified")]
        public long LastModified { get; set; }

        public virtual ICollection<SongPlaylist> SongPlaylists { get; set; }
    }
}
