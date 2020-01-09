using System;
using Database.Models.Songs;

namespace Web.Models
{
    /// <summary>
    ///     API format of category class without references to constrains elements.
    ///     This class is used for sending category in non category API.
    /// </summary>
    public class ApiCategoryPoor
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }


        public ApiCategoryPoor()
        {
            
        }
        
        /// <summary>
        ///     Constructor for building of instance of this class from its database equivalent. 
        /// </summary>
        /// <param name="databaseCategory">Database equivalent to this class.</param>
        public ApiCategoryPoor(Category databaseCategory)
        {
            Id = databaseCategory.Id;
            Name = databaseCategory.Name;
            Description = databaseCategory.Description;
            LastModifiedUtc = databaseCategory.LastModifiedDateTimeUtc;
        }
    }
}