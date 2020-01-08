using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Controller GET method to get home index page.
        /// </summary>
        /// <returns>View of index page.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Controller GET method to get privacy page
        /// </summary>
        /// <returns>View of privacy page.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        ///     Controller GET method to get error page.
        /// </summary>
        /// <returns>View of error page.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}