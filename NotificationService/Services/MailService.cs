using System.Threading.Tasks;
using NotificationService.Classes;
using NotificationService.Interfaces;

namespace NotificationService.Services
{
    public class MailService : IMailService
    {
        private readonly IEmailSender _emailSender;

        public MailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<bool> SendAsync(MailData mailData)
        {
            try
            {
                var mailMessage = new EmailMessageBuilder()
                    .SetFrom(mailData.From)
                    .AddToRecipient(mailData.To[0])
                    .SetSubject(mailData.Subject)
                    .SetBody(mailData.Body, true)
                    .Build();

                _emailSender.SendEmail(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
