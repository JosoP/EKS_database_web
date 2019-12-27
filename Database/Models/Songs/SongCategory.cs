using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.Songs
{
    [Table("song_category")]
    public partial class SongCategory
    {
        [Key]
        [Column("song")]
        public long SongId { get; set; }
        
        [ForeignKey(nameof(SongId))]
        public Song Song { get; set; }
        
        [Key]
        [Column("category")]
        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        
        

        public override string ToString()
        {
            return $"Song-Category - \"{Song.Title}\" - \"{Category.Name}\"";
        }
    }
}
