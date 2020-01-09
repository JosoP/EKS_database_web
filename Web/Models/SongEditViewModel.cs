using System.Collections.Generic;
using Database.Models.Songs;

namespace Web.Models
{
    /// <summary>
    ///     Model used for song edit and create view.
    /// </summary>
    public class SongEditViewModel
    {
        /// <summary>
        ///     Song that is currently editing or creating.
        /// </summary>
        public Song Song { get; set; }
        
        /// <summary>
        ///     Categories that are selected for current song.
        /// </summary>
        public List<Category> SelectedCategories { get; set; }
        
        /// <summary>
        ///     Other categories, that are not selected for current song.
        /// </summary>
        public List<Category> OtherCategories { get; set; }
    }
}