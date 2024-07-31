using System.Net.Mail;

namespace NotificationService.Classes
{
    public class SmtpSettings
    {
        public const string Smtp = "SmtpSettings";

        public string SmtpHost { get; set; }
        public Int32 SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }

       
    }
}
