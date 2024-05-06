using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication6.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _fromEmail;
        private readonly string _fromPassword;

        public EmailSender(string fromEmail, string fromPassword)
        {
            _fromEmail = fromEmail;
            _fromPassword = fromPassword;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_fromEmail);
            message.Subject = subject;
            message.To.Add(new MailAddress("vamsha.tamu.a21.2@icp.edu.np"));
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_fromEmail, _fromPassword),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
