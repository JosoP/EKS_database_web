using System;
using Database.Models.Songs;

namespace Web.Models
{
    /// <summary>
    ///     API format of playlist class without references to constrains elements.
    ///     This class is used for sending playlist non playlist in API.
    /// </summary>
    public class ApiPlaylistPoor
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }


        public ApiPlaylistPoor()
        {
            
        }
        
        /// <summary>
        ///     Constructor for building of instance of this class from its database equivalent. 
        /// </summary>
        /// <param name="databasePlaylist">Database equivalent to this class</param>
        public ApiPlaylistPoor(Playlist databasePlaylist)
        {
            Id = databasePlaylist.Id;
            Name = databasePlaylist.Name;
            Description = databasePlaylist.Description;
            LastModifiedUtc = databasePlaylist.LastModifiedDateTimeUtc;
        }
    }
}