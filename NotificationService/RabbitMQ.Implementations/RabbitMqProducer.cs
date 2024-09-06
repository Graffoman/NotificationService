using RabbitMQ.Abstractions;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Implementations
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private RabbitSettings _settings;
        private string _queueName;

        public RabbitMqProducer(RabbitSettings settings)
        {
            _settings = settings;
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _settings.NotificationsQueueName,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: _settings.NotificationsQueueName,
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
