using Application.DTOs.Account;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailSendDTO emailSend);
    }
}
