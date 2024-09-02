namespace RabbitMQ.Abstractions
{
    public class RabbitSettings
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string UsersQueueName { get; set; }
        public string NotificationsQueueName { get; set; }
    }
}
