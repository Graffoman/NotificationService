using NotificationService.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
        //Task<bool> SendAsync(MailMessage message, CancellationToken ct);
    }
}