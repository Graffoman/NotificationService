using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Classes
{
    internal class EmailMessageMaker
    {
        private MailMessage _mailMessage;

        public EmailMessageMaker()
        {
            _mailMessage = new MailMessage();
        }

        public EmailMessageMaker SetFrom(string from)
        {
            _mailMessage.From = new MailAddress(from);
            return this;
        }

        public EmailMessageMaker AddToRecipient(string to)
        {
            _mailMessage.To.Add(new MailAddress(to));
            return this;
        }

        public EmailMessageMaker SetSubject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public EmailMessageMaker SetBody(string body)
        {
            _mailMessage.Body = body;
            return this;
        }

        public MailMessage Build()
        {
            return _mailMessage;
        }

    }
}
