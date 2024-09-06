using NotificationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Classes
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public SmtpEmailSender(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public void SendEmail(MailMessage message)
        {
            using (var smtp = new SmtpClient(_smtpHost, _smtpPort))
            {
                smtp.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

        public Task<bool> SendAsync(MailMessage message, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
