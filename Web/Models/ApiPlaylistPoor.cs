using System;
using Database.Models.Songs;

namespace Web.Models
{
    public class ApiPlaylistPoor
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }


        public ApiPlaylistPoor()
        {
            
        }
        
        public ApiPlaylistPoor(Playlist databasePlaylist)
        {
            Id = databasePlaylist.Id;
            Name = databasePlaylist.Name;
            Description = databasePlaylist.Description;
            LastModifiedUtc = databasePlaylist.LastModifiedDateTimeUtc;
        }
    }
}