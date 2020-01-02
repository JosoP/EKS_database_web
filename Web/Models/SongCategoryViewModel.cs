using System.Collections.Generic;
using Database.Models.Songs;
using Microsoft.AspNetCore.Mvc;

namespace Web.Models
{
    public class SongCategoryViewModel
    {
        public Song Song { get; set; }
        public List<Category> Categories { get; set; }
    }
}