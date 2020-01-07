using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult TestEmail()
        {
            var from = "ekspevnik@gmail.com";
            var to = "jozkoprivarcak@gmail.com";

            SmtpClient client = new SmtpClient();
           client.DeliveryMethod = SmtpDeliveryMethod.Network;
           client.EnableSsl = true;
           client.Host = "smtp.gmail.com";
           client.Port = 587;
        
           // setup Smtp authentication
           System.Net.NetworkCredential credentials = 
               new System.Net.NetworkCredential(from, "eks123eks");
           client.UseDefaultCredentials = false;
           client.Credentials = credentials;                
        
           MailMessage msg = new MailMessage();
           msg.From = new MailAddress(from);
           msg.To.Add(new MailAddress(to));
        
           msg.Subject = "This is a test Email subject";
           msg.IsBodyHtml = true;
           msg.Body = string.Format("<html><head></head><body><b>Test HTML Email</b></body>");
        
           try
           {
               client.Send(msg);
               _logger.LogInformation($"Test email sent to {to}");
           }
           catch (Exception ex)
           {
               _logger.LogError($"Cannot send e-mail to: {to}");
               _logger.LogError(ex.Message);
           }

           return Ok();
        }
    }
}