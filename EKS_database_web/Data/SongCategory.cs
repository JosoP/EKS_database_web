using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKS_database_web.Data
{
    [Table("song_category")]
    public partial class SongCategory
    {
        [Column("song")]
        public long SongId { get; set; }
        
        [Column("category")]
        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        
        [ForeignKey(nameof(SongId))]
        public virtual Song Song { get; set; }
    }
}
