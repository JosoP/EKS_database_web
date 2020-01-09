using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Database.Models.Songs;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers.Api
{
    /// <summary>
    ///     Controller for songs API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SongsDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public SongsController(SongsDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Songs
        /// <summary>
        ///     Gets All Songs in API format.
        /// </summary>
        /// <returns>All songs in API format.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiSong>>> GetSongs()
        {
            var databaseSongs = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(sc => sc.Category)
                .Include(s => s.SongPlaylists).ThenInclude(sp => sp.Playlist)
                .ToListAsync();
            
            var apiSongs = new List<ApiSong>();
            foreach (var databaseSong in databaseSongs)
                apiSongs.Add(new ApiSong(databaseSong));

            return apiSongs;
        }

        // GET: api/Songs/5
        /// <summary>
        ///     Gets song specified by ID in API format.
        /// </summary>
        /// <param name="id">ID of song that will be returned.</param>
        /// <returns>Songs in API format.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiSong>> GetSong(long id)
        {
            var song = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(sc => sc.Category)
                .Include(s => s.SongPlaylists).ThenInclude(sp => sp.Playlist)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (song == null)    return NotFound();

            return new ApiSong(song);
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Updates song specified by ID and passed in body in API format.
        /// </summary>
        /// <param name="id">ID of song to be updated.</param>
        /// <param name="apiSong">Song according to which specified song will be updated in API format.</param>
        /// <returns>Updated song in API format.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiSong>> PutSong(long id, ApiSong apiSong)
        {
            if (id != apiSong.Id)    return BadRequest();

            var song = apiSong.ToDatabaseSong();
            
            _context.UpdateSongWithAll(song);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id)) return NotFound();

                throw;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return Conflict();
            } 

            var loadedSong = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(sc => sc.Category)
                .Include(s => s.SongPlaylists).ThenInclude(sp => sp.Playlist)
                .FirstOrDefaultAsync(s => s.Id == song.Id);
            
            return new ApiSong(loadedSong);
        }

        // POST: api/Songs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Creates new song according to song sent in request body. In this method song and its verses are created
        ///     and relations to categories and playlists are added or removed. 
        /// </summary>
        /// <param name="apiSong">Song to be created in API format.</param>
        /// <returns>Currently created song in API format.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiSong>> PostSong(ApiSong apiSong)
        {
            var song = apiSong.ToDatabaseSong();

            foreach (var songCategory in song.SongCategories)
                _context.Add(songCategory);

            foreach (var songPlaylist in song.SongPlaylists)
                _context.Add(songPlaylist);
            
            _context.Songs.Add(song);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {    
                _logger.LogWarning(e.Message);
                return Conflict();
            }

            var loadedSong = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(sc => sc.Category)
                .Include(s => s.SongPlaylists).ThenInclude(sp => sp.Playlist)
                .FirstOrDefaultAsync(s => s.Id == song.Id);
            
            return CreatedAtAction("GetSong", new { id = loadedSong.Id }, new ApiSong(loadedSong));
        }

        // DELETE: api/Songs/5
        /// <summary>
        ///     Removes a song specified by ID.
        /// </summary>
        /// <param name="id">ID of song to be removed.</param>
        /// <returns>Currently removed song in API format.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiSong>> DeleteSong(long id)
        {
            var song = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(sc => sc.Category)
                .Include(s => s.SongPlaylists).ThenInclude(sp => sp.Playlist)
                .FirstOrDefaultAsync(s => s.Id == id);
            
            if (song == null) return NotFound();
            
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return new ApiSong(song);
        }

        private bool SongExists(long id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
