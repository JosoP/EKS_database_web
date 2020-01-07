using System.Collections.Generic;
using Database.Models.Songs;

namespace Web.Models
{
    public class SongCategoryViewModel
    {
        public Song Song { get; set; }
        public List<Category> SelectedCategories { get; set; }
        public List<Category> OtherCategories { get; set; }
    }
}