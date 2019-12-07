using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKS_database_web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EKS_database_web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SongsDbContext _songDbContext;
        public List<Song> AllSongs { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SongsDbContext songDbContext)
        {
            _logger = logger;
            _songDbContext = songDbContext;
            AllSongs = _songDbContext.Songs
                .Include(song => song.Verse)
                .Include(song => song.SongCategories)
                .Include(song => song.SongPlaylists)
                .ToList();
        }

        public void OnGet()
        {
        }
    }
}