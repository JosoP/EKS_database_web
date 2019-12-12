using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models.Songs;
using EKS_database_web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EKS_database_web.Pages.Songs
{
    public class IndexModel : PageModel
    {
        private readonly SongsDbContext _songDbContext;
        public IList<Song> AllSongs { get; set; }
        
        public IndexModel(SongsDbContext songDbContext)
        {
            _songDbContext = songDbContext;
        }
        
        public async Task<IActionResult>  OnGetAsync()
        {
            AllSongs = await _songDbContext.Songs
                .Include(song => song.Verses)
                .Include(song => song.SongCategories).ThenInclude(songCastegory => songCastegory.Category)
                .Include(song => song.SongPlaylists).ThenInclude(songPlaylist => songPlaylist.Playlist)
                .Include(song => song.SongCategories)
                .ToListAsync();

            return Page();
        }
    }
}