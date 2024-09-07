using System.Net.Mail;

namespace NotificationService.Classes
{
    internal class EmailMessageBuilder
    {
        private MailMessage _emailMessage;

        public EmailMessageBuilder()
        {
            _emailMessage = new MailMessage();
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

        public EmailMessageBuilder SetBody(string body, bool isHtml = false)
        {
            _emailMessage.Body = body;
            _emailMessage.IsBodyHtml = isHtml;
            return this;
        }

        public MailMessage Build()
        {
            return _emailMessage;
        }

        public EmailMessageBuilder SetBeautifulBody(string messageText, string openQuestionnaireUrl)
        {
            string htmlBody = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            background-color: #ffffff;
                            border: 1px solid #ddd;
                            border-radius: 8px;
                            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 20px 0;
                            background-color: #007bff;
                            color: #ffffff;
                            border-radius: 8px 8px 0 0;
                        }}
                        .content {{
                            padding: 20px;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 20px 0;
                            background-color: #f8f9fa;
                            border-radius: 0 0 8px 8px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Сервис опросов</h1>
                        </div>
                        <div class='content'>
                            <p>{messageText}</p>
                            <p><a href='{openQuestionnaireUrl}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: #ffffff; text-decoration: none; border-radius: 5px;'>Нажмите для перехода к опросу</a></p>
                        </div>
                        <div class='footer'>
                            <p>С уважением,<br>Команда Сервиса опросов</p>
                        </div>
                    </div>
                </body>
                </html>
            ";

            _emailMessage.Body = htmlBody;
            _emailMessage.IsBodyHtml = true;
            return this;
        }
    }
}
