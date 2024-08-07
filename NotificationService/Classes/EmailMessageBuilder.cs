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
        private MailData _emailMessage;

        public EmailMessageBuilder(MailData emailMessage)
        {
            _emailMessage = emailMessage;
        }

        public EmailMessageBuilder SetFrom(MailAddress from)
        {
            _emailMessage.From = from;
            return this;
        }

        public EmailMessageBuilder AddToRecipient(List<string> to)
        {
            _emailMessage.To.AddRange(to);
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

        public MailData Build()
        {
            return _emailMessage;
        }
    }
}