using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models.Songs;

namespace Web.Models
{
    /// <summary>
    ///     API format of song class. This class is used for sending song in song API.
    /// </summary>
    public class ApiSong
    {
        
        public long Id { get; set; }
        
        public string Title { get; set; }
        
        public long? Number { get; set; }
        
        public string Author { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }
        
        public List<ApiVerse> Verses { get; set; }
        
        public List<ApiCategoryPoor> Categories { get; set; }
        
        public List<ApiPlaylistPoor> Playlists { get; set; }

        
        public ApiSong()
        {
            
        }
        
        /// <summary>
        ///     Constructor for building of instance of this class from its database equivalent. 
        /// </summary>
        /// <param name="databaseSong">Database equivalent to this class.</param>
        public ApiSong(Song databaseSong)
        {
            Id = databaseSong.Id;
            Title = databaseSong.Title;
            Number = databaseSong.Number;
            Author = databaseSong.Author;
            LastModifiedUtc = databaseSong.LastModifiedDateTimeUtc;
            Verses = new List<ApiVerse>();
            Categories = new List<ApiCategoryPoor>();
            Playlists = new List<ApiPlaylistPoor>();

            foreach (var databaseVerse in databaseSong.Verses)
                Verses.Add(new ApiVerse(databaseVerse));

            foreach (var songCategory in databaseSong.SongCategories)
                Categories.Add(new ApiCategoryPoor(songCategory.Category));

            foreach (var songPlaylist in databaseSong.SongPlaylists)
                Playlists.Add(new ApiPlaylistPoor(songPlaylist.Playlist));
        }

        /// <summary>
        ///     Converts instance of this class to instance of database equivalent class.
        /// </summary>
        /// <returns>Instance of database equivalent of this class.</returns>
        public Song ToDatabaseSong()
        {
            var songCategories = Categories.Select(category => new SongCategory
            {
                SongId = Id, 
                CategoryId = category.Id
            }).ToList();
            
            var songPlaylists = Playlists.Select(playlist => new SongPlaylist()
            {
                SongId = Id, 
                PlaylistId = playlist.Id
            }).ToList();

            return new Song
            {
                Id = Id,
                Title = Title,
                Number = Number,
                Author = Author,
                LastModifiedDateTimeUtc = LastModifiedUtc,
                Verses = Verses.Select(v => v.ToDatabaseVerse(Id)).ToList(),
                SongCategories = songCategories,
                SongPlaylists = songPlaylists
            };
        }
    }
}