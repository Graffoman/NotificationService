using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mail;
using RabbitMQ.Abstractions;
using NotificationService.Interfaces;
using NotificationService.Configuration;
using Microsoft.Extensions.Logging;

namespace NotificationService.Classes
{
    public class RabbitMQConsumer : IHostedService, IDisposable
    {
        private readonly IEmailSender _emailSender;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        private readonly ILogger<RabbitMQConsumer> _logger;

        public RabbitMQConsumer(IEmailSender emailSender, RabbitSettings rabbitSettings, ILogger<RabbitMQConsumer> logger)
        {
            if (string.IsNullOrEmpty(rabbitSettings.HostName))
                throw new ArgumentNullException(nameof(rabbitSettings.HostName));
            if (string.IsNullOrEmpty(rabbitSettings.UserName))
                throw new ArgumentNullException(nameof(rabbitSettings.UserName));
            if (string.IsNullOrEmpty(rabbitSettings.Password))
                throw new ArgumentNullException(nameof(rabbitSettings.Password));
            if (string.IsNullOrEmpty(rabbitSettings.QueueName))
                throw new ArgumentNullException(nameof(rabbitSettings.QueueName));

            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var factory = new ConnectionFactory()
            {
                HostName = rabbitSettings.HostName,
                UserName = rabbitSettings.UserName,
                Password = rabbitSettings.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = rabbitSettings.QueueName;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ProcessMessage(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }

        private void ProcessMessage(string message)
        {
            try
            {
                var emailMessage = JsonConvert.DeserializeObject<Notification>(message);

                var mailMessage = new EmailMessageBuilder()
                    .SetFrom("alenchaeto@mail.ru")
                    .AddToRecipient(emailMessage.Email)
                    .SetSubject("Сервис опросов")
                    .SetBody($"<html><body>{emailMessage.MessageText}<br><a href='{emailMessage.OpenQuestionnaireUrl}'>Нажмите для перехода к опросу</a></body></html>", true)
                    .Build();

                _emailSender.SendEmail(mailMessage);
                _logger.LogInformation("Message processed and email sent.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process message and send email.");
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
