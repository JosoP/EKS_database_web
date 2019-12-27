using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EKS_database_web.Data;

namespace Database.Models.Songs
{
    [Table("songs")]
    public partial class Song
    {
        public Song()
        {
            SongCategories = new HashSet<SongCategory>();
            SongPlaylists = new HashSet<SongPlaylist>();
            Verses = new List<Verse>();
        }

        [Key]
        [Column("_id")]
        public long Id { get; set; }
        
        [Required]
        [DisplayName("Názov")]
        [Column("title")]
        public string Title { get; set; }
        
        [DisplayName(" Číslo")]
        [Column("number")]
        public long? Number { get; set; }
        
        [DisplayName("Autor")]
        [Column("author")]
        public string Author { get; set; }
        
        [DisplayName("Naposledy upravené")]
        [Column("lastModified")]
        public long LastModified { get; set; }

        [NotMapped]
        [DisplayName("Naposledy upravené")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}")]
        public DateTime LastModifiedDateTimeLocal
        {
            get => DateTimeOffset.FromUnixTimeSeconds(LastModified).DateTime.ToLocalTime();
            set
            {
                DateTimeOffset offset = DateTime.SpecifyKind(value, DateTimeKind.Local);
                LastModified = offset.ToUnixTimeSeconds();
            }
        }
        
        [DisplayName("Kategórie")]
        public virtual ICollection<SongCategory> SongCategories { get; set; }
        
        [DisplayName("Playlisty")]
        public virtual ICollection<SongPlaylist> SongPlaylists { get; set; }
        
        [DisplayName("Strofy")]
        public virtual ICollection<Verse> Verses { get; set; }

        public override string ToString()
        {
            return
                $"Song - Id=\"{Id}\", Title=\"{Title}\", Number=\"{Number}\", Author=\"{Author}\"," +
                $"categories={SongCategories.Count}, playlists={SongPlaylists.Count}, " +
                $"verses={Verses.Count}, LastModified=\"{LastModifiedDateTimeLocal}\"";
        }
    }
}
