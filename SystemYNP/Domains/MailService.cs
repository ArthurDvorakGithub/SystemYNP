using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace SystemYNP.Domains
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string from, string to, string subject, string text)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Text) { Text = text };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["Smtp:Host"], _configuration.GetSection("Smtp:Port").Get<int>(), SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}