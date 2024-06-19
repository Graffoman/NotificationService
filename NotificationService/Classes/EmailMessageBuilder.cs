using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Classes
{
    internal class EmailMessageBuilder
    {
        private MailMessage _mailMessage;

        public EmailMessageBuilder()
        {
            _mailMessage = new MailMessage();
        }

        public EmailMessageBuilder SetFrom(string from)
        {
            _mailMessage.From = new MailAddress(from);
            return this;
        }

        public EmailMessageBuilder AddToRecipient(string to)
        {
            _mailMessage.To.Add(new MailAddress(to));
            return this;
        }

        public EmailMessageBuilder SetSubject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public EmailMessageBuilder SetBody(string body)
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