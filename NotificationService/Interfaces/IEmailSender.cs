using System.Net.Mail;

namespace NotificationService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
        Task<bool> SendAsync(MailMessage message, CancellationToken ct);
    }
}
