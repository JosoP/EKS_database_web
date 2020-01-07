using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EKS_database_web.Data;

namespace Database.Models.Songs
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
        [DisplayName("Názov")]
        public string Name { get; set; }
        
        [Column("description")]
        [DisplayName("Popis")]
        public string Description { get; set; }
        
        [Column("lastModified")]
        [DisplayName("Naposledy upravené")]
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
        
        [JsonIgnore]
        [DisplayName("Piesne")]
        public virtual ICollection<SongPlaylist> SongPlaylists { get; set; }
    }
}
