using System.Collections.Generic;
using System.Threading.Tasks;
using EKS_database_web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EKS_database_web.Pages
{
    public class Songs : PageModel
    {
        private readonly SongsDbContext _songDbContext;
        public IList<Song> AllSongs { get; set; }
        
        public Songs(SongsDbContext songDbContext)
        {
            _songDbContext = songDbContext;
        }
        
        public async Task OnGetAsync()
        {
            AllSongs = await _songDbContext.Songs
                .Include(song => song.Verses)
                .Include(song => song.SongCategories).ThenInclude(songCastegory => songCastegory.Category)
                .Include(song => song.SongPlaylists).ThenInclude(songPlaylist => songPlaylist.Playlist)
                .Include(song => song.SongCategories)
                .ToListAsync();
        }
    }
}