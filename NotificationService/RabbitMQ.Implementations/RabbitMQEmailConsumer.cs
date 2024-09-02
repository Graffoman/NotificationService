using RabbitMQ;
using RabbitMQ.Abstractions;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Implementations
{
public class RabbitMqEmailConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitSettings _settings;
    private readonly ILogger<RabbitMqEmailConsumer> _logger;

    public RabbitMqEmailConsumer(RabbitSettings settings, ILogger<RabbitMqEmailConsumer> logger)
    {
        _settings = settings;
        _logger = logger;

        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: _settings.EmailQueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            _logger.LogInformation($"Received message from queue: {_settings.EmailQueueName}");

            try
            {
                var emailMessage = JsonConvert.DeserializeObject<EmailMessageDto>(content);
                await SendEmailAsync(emailMessage);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Failed to process the email message: {content}", content, e);
            }

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(_settings.EmailQueueName, false, consumer);

        return Task.CompletedTask;
    }

    private async Task SendEmailAsync(EmailMessageDto emailMessage)
    {
        // Implement your email sending logic here
        foreach (var recipient in emailMessage.Recipients)
        {
            // Send email to each recipient
            _logger.LogInformation($"Sending email to {recipient} with subject '{emailMessage.Subject}'");
            // Example:
            // await _emailService.SendEmailAsync(recipient, emailMessage.Subject, emailMessage.Body);
        }
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
}