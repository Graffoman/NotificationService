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
        private EmailMessage _emailMessage;

        public EmailMessageBuilder()
        {
            _emailMessage = new EmailMessage();
        }

        public EmailMessageBuilder SetFrom(string from)
        {
            _emailMessage.From = new MailAddress(from);
            return this;
        }

        public EmailMessageBuilder AddToRecipient(string to)
        {
            _emailMessage.To.Add(new MailAddress(to));
            return this;
        }

        public EmailMessageBuilder SetSubject(string subject)
        {
            _emailMessage.Subject = subject;
            return this;
        }

        public EmailMessageBuilder SetBody(string body)
        {
            _emailMessage.Body = body;
            return this;
        }

        public EmailMessage Build()
        {
            return _emailMessage;
        }
    }
}