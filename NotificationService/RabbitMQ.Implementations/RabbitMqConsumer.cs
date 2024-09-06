using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mail;
using RabbitMQ.Abstractions;
using NotificationService.Interfaces;

namespace NotificationService.Classes
{
    public class RabbitMQConsumer
    {
        private readonly IEmailSender _emailSender;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQConsumer(IEmailSender emailSender, string hostName, string username, string password)
        {
            _emailSender = emailSender;
            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = username,
                Password = password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void StartConsuming(string queueName)
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ProcessMessage(message);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        private void ProcessMessage(string message)
        {
            var emailMessage = JsonConvert.DeserializeObject<Notification>(message);

            var mailMessage = new EmailMessageBuilder()
                .SetFrom("alenchaeto@mail.ru")
                .AddToRecipient(emailMessage.Email)
                .SetSubject("Сервис опросов")
                .SetBody($"<html><body>{emailMessage.MessageText}<br><a href='{emailMessage.OpenQuestionnaireUrl}'>Нажмите, чтобы открыть опрос</a></body></html>", true)
                .Build();

            try
            {
                _emailSender.SendEmail(mailMessage);
                Console.WriteLine("Message sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
