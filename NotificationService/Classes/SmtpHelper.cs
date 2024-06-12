using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Classes
{
    public class SmtpHelper
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public SmtpHelper(string smtpServer, int smtpPort, string senderEmail, string senderPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
        }

        public void SendEmail(string recipientEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);

                var message = new MailMessage
                {
                    From = new MailAddress(_senderEmail),
                    To = { new MailAddress(recipientEmail) },
                    Subject = subject,
                    Body = body
                };

                client.Send(message);
            }
        }
    }
}
