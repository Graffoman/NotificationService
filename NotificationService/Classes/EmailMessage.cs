using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Classes
{
    public class EmailMessage
    {
        public MailAddress From { get; set; }
        public List<MailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailMessage()
        {
            To = new List<MailAddress>();
        }
        public MailMessage ToMailMessage()
        {
            var mailMessage = new MailMessage();
            mailMessage.From = From;
            foreach (var to in To)
            {
                mailMessage.To.Add(to);
            }
            mailMessage.Subject = Subject;
            mailMessage.Body = Body;
            return mailMessage;
        }
    }
}