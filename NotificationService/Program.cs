using NotificationService.Classes;
using NotificationService.Interfaces;
using System;
using System.Net.Mail;
using NotificationService.Configuration;

namespace NetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mailSettings = new MailSettings
            {
                Host = "smtp.mail.ru",
                Port = 587,
                UserName = "alenchaeto@mail.ru",
                Password = "JMvwj6tD3r6ACGypqLNq",
                UseSSL = true
            };

            IEmailSender emailSender = new SmtpEmailSender(mailSettings.Host, mailSettings.Port, mailSettings.UserName, mailSettings.Password);

            var rabbitMQConsumer = new RabbitMQConsumer(emailSender, "84.201.158.212", "admin", "admin");
            rabbitMQConsumer.StartConsuming("Notifications");

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
