using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.Models.Songs;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using Web.Models;

namespace Web.Controllers
{
    public class SongsController : Controller
    {
        private readonly SongsDbContext _context;

        public SongsController(SongsDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// GET: Songs
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string searchString)
        {
            var songs = _context.Songs
                .Include(song => song.SongCategories).ThenInclude(songCategory => songCategory.Category)
                .Select(song => song);

            if (!String.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.Title.ToLower().Contains(searchString.ToLower()));
            }
            
            return View(await songs.ToListAsync());
        }

        // GET: Songs/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(songCategory => songCategory.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            song.Verses = song.Verses.OrderBy(v => v.SequenceNumber).ToList();

            return View(song);
        }

        // GET: Songs/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Number,Author,LastModified")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new SongCategoryViewModel
            {
                Song = await _context.Songs
                    .Include(s => s.Verses)
                    .Include(s => s.SongCategories).ThenInclude(songCategory => songCategory.Category)
                    .FirstOrDefaultAsync(m => m.Id == id),
                Categories = await _context.Categories.ToListAsync()
            };
            if (viewModel.Song == null)
            {
                return NotFound();
            }
            
            viewModel.Song.Verses = viewModel.Song.Verses.OrderBy(v => v.SequenceNumber).ToList();
            
            
            
            return View(viewModel);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            long id, 
            [Bind("Song,Categories")] SongCategoryViewModel viewModel)
        {
            if (id != viewModel.Song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                viewModel.Song.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();
                for (int i = 0; i < viewModel.Song.Verses.Count; i++)
                {
                    viewModel.Song.Verses[i].SequenceNumber = i;
                    viewModel.Song.Verses[i].SongId = viewModel.Song.Id;
                }
                
                try
                {
                    _context.UpdateSong(viewModel.Song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(viewModel.Song.Id))
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
            
            return View(viewModel);
        }

        // GET: Songs/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var song = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories)
                .Include(s => s.SongPlaylists)
                .FirstOrDefaultAsync(m => m.Id == id);

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool SongExists(long id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
