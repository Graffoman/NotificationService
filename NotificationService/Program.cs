using Microsoft.Extensions.DependencyInjection;
using NotificationService.Classes;
using NotificationService.Services;
using System.Net.Mail;
using NotificationService.Configuration;
using RabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.Configure<RabbitSettings>(builder.Configuration.GetSection("RabbitMQ"));
       //builder.Services.AddHostedService<RabbitMqEmailConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




