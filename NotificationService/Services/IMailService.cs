using NotificationService.Classes;

namespace NotificationService.Services
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData);
    }
}