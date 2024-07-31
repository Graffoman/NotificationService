//using RabbitMQ.Client;
//using NotificationService.Classes;
//using NotificationService.Interfaces;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
//using System.Net.Mail;
//using RabbitMQ.Client;

//namespace NotificationService
//{
//    public class Class
//    {
//        public void Main1(string[] args)
//        {
//            IConfiguration configuration = new ConfigurationBuilder()
//                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//                .Build();
//            var connection = GetRabbitConnection(configuration);
//            var channel = connection.CreateModel();


//        }

//        public  IConnection GetRabbitConnection(IConfiguration configuration)
//        {

//            IConfiguration
//            ConnectionFactory factory = new ConnectionFactory
//            {
//                HostName = rmqSettings.Host;
//                VirtualHost = rmqSettings.VHost;
//                UserName = rmqSettings.Login;
//                HostName = rmqSettings.Host;
//            }
//            var conn = cf.newConnection();
//            var ch = conn.CreateModel();


//            ch.Close();
//        }


//    }
