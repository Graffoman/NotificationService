namespace RabbitMQ.Abstractions
{
    public interface IRabbitMqProducer
    {
        void SendMessage(string message);
    }
}
