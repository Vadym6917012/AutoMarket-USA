using AutoMarket.Server.Shared.DTOs.Account;
using Microsoft.Extensions.Configuration;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using MailKit.Net.Smtp;
using MailKit;
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
            //MailjetClient client = new MailjetClient(_config["MailJet:ApiKey"], _config["MailJet:SecretKey"]);

            //var email = new TransactionalEmailBuilder()
            //    .WithFrom(new SendContact(_config["Email:From"], _config["Email:ApplicationName"]))
            //    .WithSubject(emailSend.Subject)
            //    .WithHtmlPart(emailSend.Body)
            //    .WithTo(new SendContact(emailSend.To))
            //    .Build();

            //var response = await client.SendTransactionalEmailAsync(email);
            //if (response.Messages != null)
            //{
            //    if (response.Messages[0].Status == "success")
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }
    }
}
