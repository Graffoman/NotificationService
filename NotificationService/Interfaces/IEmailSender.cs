using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
        Task<bool> SendAsync(MailMessage message, CancellationToken ct);
    }
}
