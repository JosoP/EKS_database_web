using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers
{
    public class SongsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}