using RabbitMQ;
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

    
    }
}
