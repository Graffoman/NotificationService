using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Classes
{
    public class MailData
    {
        //receiver
        public List<string> To { get; set; }
        public List<string> Bcc { get; }
        public List<string> Cc { get; }

        //sender
        public MailAddress From { get; set; }
        public string DisplayName { get; }
        public string ReplyTo { get; }
        public string ReplyToName { get; }

        // Content
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailData(List<string> to,
            string? subject,
            string? body = null,
            MailAddress? from = null,
            string? displayName = null,
            string? replyTo = null,
            string? replyToName = null,
            List<string>? bcc = null,
            List<string>? cc = null)
        {
            //Receiver
            To = new List<string>();
            Bcc = bcc ?? new List<string>();
            Cc = cc ?? new List<string>();

            // Sender
            From = from;
            DisplayName = displayName;
            ReplyTo = replyTo;
            ReplyToName = replyToName;

            // Content
            Subject = subject;
            Body = body;

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