using System;
using Database.Models.Songs;

namespace Web.Models
{
    public class ApiCategoryPoor
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }


        public ApiCategoryPoor()
        {
            
        }
        
        public ApiCategoryPoor(Category databaseCategory)
        {
            Id = databaseCategory.Id;
            Name = databaseCategory.Name;
            Description = databaseCategory.Description;
            LastModifiedUtc = databaseCategory.LastModifiedDateTimeUtc;
        }
    }
}