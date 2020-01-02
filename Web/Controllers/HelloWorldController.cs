using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }

        // 
        // // GET: /HelloWorld/Welcome/ 
        // public string Welcome()
        // {
        //     return "This is the Welcome action method...";
        // }
        
        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}