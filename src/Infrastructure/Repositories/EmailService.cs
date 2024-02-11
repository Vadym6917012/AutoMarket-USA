using Application.Common.Interfaces;
using Application.DTOs.Account;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace Infrastructure.Repositories
{
    public class EmailService : IEmailService
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

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = GetEmailTemplate(emailSend.Body);

            //email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //{
            //    Text = emailSend.Body
            //};

            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_config["Email:Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_config["Email:From"], _config["Email:Password"]);

                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            return true;
        }

        private string GetEmailTemplate(string message)
        {
            string template = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title></title>
</head>
<body style=""margin: 0; padding: 0; font-family: Arial, sans-serif;"">
    <table width=""100%"" bgcolor=""#ff8500"" style=""background-color: #ff8500;"">
        <tr>
            <td style=""padding: 20px 0; text-align: center;"">
                <h1 style=""color: #ffffff;"">AutoMarket</h1>
            </td>
        </tr>
    </table>
    <table width=""100%"" style=""background-color: #ffffff;"">
        <tr>
            <td style=""padding: 20px;"">
                {message}
            </td>
        </tr>
    </table>
    <table width=""100%"" bgcolor=""#f1f1f1"" style=""background-color: #f1f1f1;"">
        <tr>
            <td style=""padding: 20px; text-align: center;"">
                <p style=""color: #666666;"">З найкращими побажаннями, адміністрація AutoMarket</p>
            </td>
        </tr>
    </table>
</body>
</html>
";

            template = template.Replace("{message}", message);

            return template;
        }
    }
}
