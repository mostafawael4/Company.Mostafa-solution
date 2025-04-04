using Company.Mostafa.DAL.Models;
using Company.Mostafa.PL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Company.Mostafa.PL.Helpers
{
    public class MailService(IOptions<MailSetting> _options) : IMailService
    {
        public void SendEmail(Email email)
        {
            // Bulid Message
            var mail = new MimeMessage();
            mail.Subject = email.Subject;
            mail.From.Add(new MailboxAddress( _options.Value.DisplayName, _options.Value.Email));
            mail.To.Add(MailboxAddress.Parse(email.To));

            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body = builder.ToMessageBody();


            // establish connection

            using var smtp = new SmtpClient();
            smtp.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Value.Email, _options.Value.Password);

            // Send Message
            smtp.Send(mail);
        }
    }
}
