// See https://aka.ms/new-console-template for more information

using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace NetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //smtp сервер
            string smtpHost = "smtp.gmail.com";
            //smtp порт
            int smtpPort = 587;
            //логин
            string login = "alenchaeto@gmail.com";
            //пароль
            string pass = "-seKtd2S3N9e1";
            
            // подключение
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(login, pass);
            
            // отправитель 
            MailAddress from = new MailAddress("alenchaeto@gmail.com", "Tom");
            // кому 
            MailAddress to = new MailAddress("alenchaeto@mail.ru");
            
            //  сообщениe
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html 
            m.IsBodyHtml = true;

            try
            {
                client.Send(m);
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


