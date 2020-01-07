using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Database.Models.Songs
{
    [Table("verses")]
    public partial class Verse
    {
        [Key]
        [Column("_id")]
        public long Id { get; set; }
        
        [Column("songId")]
        public long SongId { get; set; }
        
        [Column("seqenceNumber")]
        public int SequenceNumber { get; set; }
        
        [Required]
        [DisplayName("Názov")]
        [Column("title")]
        public string Title { get; set; }
        
        [Required]
        [DisplayName("Text")]
        [Column("text")]
        public string Text { get; set; }

        [JsonIgnore]
        [DisplayName("Text")]
        [ForeignKey(nameof(SongId))]
        public virtual Song Song { get; set; }
    }
}
