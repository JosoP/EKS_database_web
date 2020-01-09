using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Database.Models.Songs;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    /// <summary>
    ///     Controller for categories pages.
    /// </summary>
    public class CategoriesController : Controller
    {
        private readonly SongsDbContext _context;

        public CategoriesController(SongsDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        /// <summary>
        ///     Controller GET method to get page with a list of all categories.
        /// </summary>
        /// <returns>View of categories Index page.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        /// <summary>
        ///     Controller GET method to get detail page of category according to specified ID.
        /// </summary>
        /// <param name="id">ID of category which detail page will be displayed.</param>
        /// <returns>View of category detail page</returns>
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        /// <summary>
        ///     Controller GET method to get page for creating of new Cateogry.
        /// </summary>
        /// <returns>View of category create page</returns>
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Controller POST method to store newly created category.
        /// </summary>
        /// <param name="category">Data of newly created category.</param>
        /// <returns>When everything goes OK, redirection to Index page, otherwise page to create the same category.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        /// <summary>
        ///     Controller GET method to get page for editing of category specified by ID.
        /// </summary>
        /// <param name="id">ID of category to be edited</param>
        /// <returns>View of categories edit page</returns>
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Controller POST method to store changes made by editing.
        /// </summary>
        /// <param name="id">ID of category to be edited.</param>
        /// <param name="category">Data of edited category.</param>
        /// <returns>When everything goes OK, redirection to Index page, otherwise page to edit the same category.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description,LastModified")]
            Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Categories/Delete/5
        /// <summary>
        ///     Controller GET method to get page to confirm deletion of a category specified by ID.
        /// </summary>
        /// <param name="id">ID of category to be removed.</param>
        /// <returns>View of category delete page.</returns>
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        /// <summary>
        ///     Controller POST method to remove category specified by ID.
        /// </summary>
        /// <param name="id">ID of category to be deleted.</param>
        /// <returns>Redirection to categories Index page.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(long id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}