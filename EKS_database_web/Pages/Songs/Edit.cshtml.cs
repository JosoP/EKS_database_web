using System.Threading.Tasks;
using EKS_database_web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EKS_database_web.Pages.Songs
{
    public class Edit : PageModel
    {
        private readonly SongsDbContext _songDbContext;

        public Song EditedSong { get; set; }
        
        public Edit(SongsDbContext songDbContext)
        {
            _songDbContext = songDbContext;
        }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditedSong = await _songDbContext.Songs.FirstOrDefaultAsync(song => song.Id == id);

            if (EditedSong == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}