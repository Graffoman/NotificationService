
using NotificationService.Classes;

namespace mailService.Services;

public interface IMailService
{
    Task<bool> SendAsync(MailData mailData);
}