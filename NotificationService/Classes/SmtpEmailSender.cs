using Microsoft.Extensions.Options;
using NotificationService.Interfaces;
using System.Net.Mail;
using System.Net;
using NotificationService.Configuration;

namespace NotificationService.Classes
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptions<MailSettings> mailSettings, ILogger<SmtpEmailSender> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public void SendEmail(MailMessage message)
        {
            try
            {
                using (var smtp = new SmtpClient(_mailSettings.Host, _mailSettings.Port))
                {
                    smtp.Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password);
                    smtp.EnableSsl = _mailSettings.UseSSL;
                    smtp.Send(message);
                    _logger.LogInformation("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email.");
                throw;
            }
        }

        public Task<bool> SendAsync(MailMessage message, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
