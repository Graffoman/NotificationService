using NotificationService.Classes;
using NotificationService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Net.Mail;
using MailKit;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using NotificationService.Configuration;
using NotificationService;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailSettings>(
    builder
        .Configuration
        .GetSection(nameof(MailSettings))
);
builder.Services.AddTransient<IMailService, MailService>();

//IEmailSender emailSender = new SmtpEmailSender("smtp.mail.ru", 587, "alenchaeto@mail.ru", "JMvwj6tD3r6ACGypqLNq");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//var emailMessageItem = new MailSettings
//{
//    From = "alenchaeto@mail.ru",
//    To = "alenchaeto@mail.ru",
//    Subject = "1",
//    Body = "2"
//};

//app.MapPost("/sendEmail", () =>
//{
//    var emailMessage = new EmailMessageBuilder()
//        .SetFrom(emailMessageItem.From)
//        .AddToRecipient(emailMessageItem.To)
//        .SetSubject(emailMessageItem.Subject)
//        .SetBody(emailMessageItem.Body)
//        .Build(); 
    
//    emailSender.SendEmail(emailMessage.ToMailMessage());
//    return Results.Ok();
//});

//app.Run();


