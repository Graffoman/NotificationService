// See https://aka.ms/new-console-template for more information

using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;

namespace NetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new System.Net.NetworkCredential("alenchaeto@mail.ru", "JMvwj6tD3r6ACGypqLNq");
            smtp.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("alenchaeto@mail.ru");
            mail.To.Add(new MailAddress("alenchaeto@mail.ru"));
            mail.Subject = "Subject";
            mail.Body = "Body";
           
            smtp.Send(mail);
            
            try
            {
                smtp.Send(mail);
                Console.WriteLine("Message send");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }

        }
    }
}


