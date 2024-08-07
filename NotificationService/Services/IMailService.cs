
using NotificationService.Classes;
using MailKit;
using MimeKit;

namespace NotificationService.Services;

public interface IMailService
{
    Task<bool> SendAsync(MailData mailData);
}