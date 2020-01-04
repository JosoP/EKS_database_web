using System.Collections.Generic;
using Database.Models.Songs;

namespace Web.Models
{
    public class AllSongsAllCategoriesViewModel
    {
        public List<Song> Songs { get; set; }
        public List<Category> Categories { get; set; }
    }
}