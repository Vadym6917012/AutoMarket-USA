using AutoMarket.Server.Shared.DTOs.Account;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace AutoMarket.Server.Infrastructure
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailAsync(EmailSendDTO emailSend)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_config["Email:From"]);
            email.To.Add(MailboxAddress.Parse(emailSend.To));
            email.Subject = emailSend.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailSend.Body
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_config["Email:Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_config["Email:From"], _config["Email:Password"]);

                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            return true;
        }
    }
}
