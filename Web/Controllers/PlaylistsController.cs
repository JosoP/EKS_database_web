using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.Models.Songs;
using Web.Models;

namespace Web.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly SongsDbContext _context;

        public PlaylistsController(SongsDbContext context)
        {
            _context = context;
        }

        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Playlists.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.SongPlaylists).ThenInclude(sp => sp.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        public async Task<IActionResult> Create()
        {
            return View(await GetPlaylistEditViewModel(new Playlist()));
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Playlist, SelectedSongs")] PlaylistEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                fillPlaylistWithSongs(viewModel.Playlist, viewModel.SelectedSongs);
                viewModel.Playlist.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();
                
                _context.Add(viewModel.Playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(await GetPlaylistEditViewModel(viewModel.Playlist));     
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.SongPlaylists)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(await GetPlaylistEditViewModel(playlist));
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Playlist, SelectedSongs")] PlaylistEditViewModel viewModel)
        {
            if (id != viewModel.Playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                fillPlaylistWithSongs(viewModel.Playlist, viewModel.SelectedSongs);
                
                try
                {
                    _context.UpdatePlaylistWithSongs(viewModel.Playlist);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(viewModel.Playlist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(await GetPlaylistEditViewModel(viewModel.Playlist));
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.SongPlaylists).ThenInclude(sp => sp.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);    
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(long id)
        {
            return _context.Playlists.Any(e => e.Id == id);
        }


        private void fillPlaylistWithSongs(Playlist playlist, List<Song> songs)
        {
            if (songs != null)
            {
                foreach (var selectedSong in songs)
                {
                    playlist.SongPlaylists.Add(new SongPlaylist
                    {
                        PlaylistId = playlist.Id,
                        SongId = selectedSong.Id
                    });
                }
            }
        }

        private async Task<PlaylistEditViewModel> GetPlaylistEditViewModel(Playlist playlist)
        {
            var viewModel = new PlaylistEditViewModel
            {
                Playlist = playlist,
                SelectedSongs = new List<Song>(),
                SongsTableViewModel = new SongsTableViewModel
                {
                    ViewMode = SongsTableViewModel.Mode.Selecting,
                    Songs = await _context.Songs
                        .Include(song => song.SongCategories).ThenInclude(songCategory => songCategory.Category)
                        .OrderBy(s => s.Title.ToLower()).ToListAsync(),
                    Categories = await _context.Categories
                        .OrderBy(c => c.Name.ToLower())
                        .ToListAsync()
                }
            };

            foreach (var songPlaylist in playlist.SongPlaylists)
            {
                var song = viewModel.SongsTableViewModel.Songs.FirstOrDefault(s => s.Id == songPlaylist.SongId);
                if (song != null)
                {
                    viewModel.SongsTableViewModel.Songs.Remove(song);
                    viewModel.SelectedSongs.Add(song);
                }
            }

            return viewModel;
        }
    }
}
