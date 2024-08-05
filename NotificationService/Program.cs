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
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SmtpSettings>(
    builder
        .Configuration
        .GetSection("SmtpSettings")
);
builder.Services.AddTransient<IMailService, MailService>();
var app = builder.Build();

var toList = new List<string>();
var bccList = new List<string>();
var ccList = new List<string>();

var emailMessageItem = new MailData(toList,
    " subject",
     "body ",
    MailAddress.C ? from = null,
    "displayName" ,
     "replyTo" ,
     "replyToName" ,
    bccList,
    ccList)

;

var emailBuilder = new EmailMessageBuilder();
emailBuilder.SetFrom(emailMessageItem.From)
        .AddToRecipient(emailMessageItem.To)
        .SetSubject(emailMessageItem.Subject)
        .SetBody(emailMessageItem.Body)
        .Build();

var mailData = emailBuilder.Build();

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


