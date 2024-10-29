using MimeKit;
using SecureLogin2FA.Domain.Interfaces.Services;
using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace SecureLogin2FA.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _gmailAddress = "charito106sebas@gmail.com";
        private readonly string _gmailPassword = "ddgcdnkuspobmova";

        public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("TEST", _gmailAddress));
                emailMessage.To.Add(new MailboxAddress("", to));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_gmailAddress, _gmailPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
