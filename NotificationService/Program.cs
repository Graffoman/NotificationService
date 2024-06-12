using NotificationService.Classes;
using System;
using System.Net.Mail;

namespace NetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var smtpClient = new SmtpClient("smtp.mail.ru", 587)
            {
                Credentials = new System.Net.NetworkCredential("alenchaeto@mail.ru", "JMvwj6tD3r6ACGypqLNq"),
                EnableSsl = true
            };
            
            var emailSender = new EmailSender(smtpClient);

            var emailMessage = new EmailMessageMaker()
                .SetFrom("alenchaeto@mail.ru")
                .AddToRecipient("alenchaeto@mail.ru")
                .SetSubject("Subject1")
                .SetBody("Body1")
                .Build();

            try
            {
                emailSender.SendEmail(emailMessage);
                Console.WriteLine("Message sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }
    }
}