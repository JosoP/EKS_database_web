using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKS_database_web.Data
{
    [Table("songs")]
    public partial class Song
    {
        public Song()
        {
            SongCategories = new HashSet<SongCategory>();
            SongPlaylists = new HashSet<SongPlaylist>();
            Verses = new HashSet<Verse>();
        }

        [Key]
        [Column("_id")]
        public long Id { get; set; }
        
        [Required]
        [Column("title")]
        public string Title { get; set; }
        
        [Column("number")]
        public long? Number { get; set; }
        
        [Column("author")]
        public string Author { get; set; }
        
        [Column("lastModified")]
        public long LastModified { get; set; }

        public virtual ICollection<SongCategory> SongCategories { get; set; }
        
        public virtual ICollection<SongPlaylist> SongPlaylists { get; set; }
        
        public virtual ICollection<Verse> Verses { get; set; }
    }
}
