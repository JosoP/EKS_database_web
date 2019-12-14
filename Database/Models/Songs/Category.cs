using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EKS_database_web.Data;

namespace Database.Models.Songs
{
    [Table("categories")]
    public partial class Category
    {
        public Category()
        {
            SongCategories = new HashSet<SongCategory>();
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

        [InverseProperty(nameof(SongCategory.Category))]
        public virtual ICollection<SongCategory> SongCategories { get; set; }

        public override string ToString()
        {
            return $"Category - Id={Id}, Name={Name}, Description={Description}, songs={SongCategories.Count}";
        }
    }
}
