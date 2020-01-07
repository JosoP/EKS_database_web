using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models.Songs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: Songs
        /// <summary>
        ///     Controller GET method to get page with a list of all songs.
        /// </summary>
        /// <returns>View of Songs Index page</returns>
        public async Task<IActionResult> Index()
        {
            var viewModel = new SongsTableViewModel
            {
                ViewMode = SongsTableViewModel.Mode.Maintaining,
                Songs = await _context.Songs
                    .Include(song => song.SongCategories).ThenInclude(songCategory => songCategory.Category)
                    .OrderBy(s => s.Title.ToLower()).ToListAsync(),
                Categories = await _context.Categories
                    .OrderBy(c => c.Name.ToLower())
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Songs/Details/5
        /// <summary>
        ///     Controller GET method to get detail page of song according to specified ID.
        /// </summary>
        /// <param name="id">ID of song which detail page will be displayed</param>
        /// <returns>View of songs detail page</returns>
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var song = await _context.Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories).ThenInclude(songCategory => songCategory.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null) return NotFound();

            song.Verses = song.Verses.OrderBy(v => v.SequenceNumber).ToList();

            return View(song);
        }

        // GET: Songs/Create
        /// <summary>
        ///     Controller GET method to get page for creating of new Song.
        /// </summary>
        /// <returns>View of songs create page</returns>
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new SongCategoryViewModel
            {
                Song = new Song(),
                OtherCategories = _context.Categories
                    .OrderBy(c => c.Name.ToLower())
                    .ToList(),
                SelectedCategories = new List<Category>()
            };

            viewModel.Song.Verses.Add(new Verse());

            return View(viewModel);
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Controller POST method to store newly created song.
        /// </summary>
        /// <param name="viewModel">Data of newly created song.</param>
        /// <returns>When everything goes OK, redirection to Index page, otherwise page to create the same song.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Song,SelectedCategories")] SongCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.SelectedCategories != null)
                {
                    foreach (var category in viewModel.SelectedCategories)
                    {
                        viewModel.Song.SongCategories.Add(new SongCategory
                        {
                            Song = viewModel.Song,
                            CategoryId = category.Id
                        });
                    }
                }

                viewModel.Song.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();

                _context.Songs.Add(viewModel.Song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Songs/Edit/5
        /// <summary>
        ///     Controller GET method to get page for editing of song specified by ID.
        /// </summary>
        /// <param name="id">ID of song to be edited</param>
        /// <returns>View of songs edit page</returns>
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var viewModel = new SongCategoryViewModel
            {
                Song = await _context.Songs
                    .Include(s => s.Verses)
                    .Include(s => s.SongCategories).ThenInclude(songCategory => songCategory.Category)
                    .FirstOrDefaultAsync(m => m.Id == id),
                OtherCategories = await _context.Categories
                    .OrderBy(c => c.Name.ToLower())
                    .ToListAsync()
            };
            if (viewModel.Song == null) return NotFound();

            viewModel.Song.Verses = viewModel.Song.Verses.OrderBy(v => v.SequenceNumber).ToList();

            viewModel.SelectedCategories = new List<Category>();
            foreach (var songCategory in viewModel.Song.SongCategories)
            {
                viewModel.OtherCategories.Remove(songCategory.Category);
                viewModel.SelectedCategories.Add(songCategory.Category);
            }

            return View(viewModel);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Controller POST method to store changes made by editing.
        /// </summary>
        /// <param name="id">ID of song to be edited</param>
        /// <param name="viewModel">Data of edited song</param>
        /// <returns>When everything goes OK, redirection to Index page, otherwise page to edit the same song.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(
            long id,
            [Bind("Song,SelectedCategories")] SongCategoryViewModel viewModel)
        {
            if (id != viewModel.Song.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (viewModel.SelectedCategories != null)
                {
                    foreach (var category in viewModel.SelectedCategories)
                    {
                        viewModel.Song.SongCategories.Add(new SongCategory
                            {SongId = viewModel.Song.Id, CategoryId = category.Id});
                    }
                }

                try
                {
                    _context.UpdateSongWithVersesAndCategories(viewModel.Song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(viewModel.Song.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Songs/Delete/5
        /// <summary>
        ///     Controller GET method to get page to confirm deletion of a song specified by ID.
        /// </summary>
        /// <param name="id">ID of song to be removed</param>
        /// <returns>View of songs delete page.</returns>
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null) return NotFound();

            return View(song);
        }

        // POST: Songs/Delete/5
        /// <summary>
        ///     Controller POST method to remove song specified by ID.
        /// </summary>
        /// <param name="id">ID of song to be deleted.</param>
        /// <returns>Redirection to songs Index page.</returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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