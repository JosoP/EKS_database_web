using System.Collections.Generic;
using System.Linq;
using Database.Models.Songs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers.Api
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class SongsController : Controller
    {
        private readonly SongsDbContext _context;

        public SongsController(SongsDbContext context)
        {
            _context = context;
        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IEnumerable<Song> Get()
        {
            return _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(sc => sc.Category)
                .Include(s => s.SongPlaylists).ThenInclude(sp => sp.Playlist)
                .ToList();
        }
        
    }
}