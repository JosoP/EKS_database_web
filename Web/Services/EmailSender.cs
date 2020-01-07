using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Web.Controllers;

namespace Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.GmailKey, subject, message, email);
        }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(Options.GmailUser, "EKS"),
                    To = { new MailAddress(email)},
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(Options.GmailUser, apiKey);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                    _logger.LogInformation($"Email sent to {email}");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Cannot send e-mail to: {email}");
                _logger.LogError(ex.Message);
            }
        }
    }
}