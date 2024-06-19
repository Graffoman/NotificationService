using NotificationService.Classes;
using NotificationService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>(sp =>
    new SmtpEmailSender("smtp.mail.ru", 587, "alenchaeto@mail.ru", "JMvwj6tD3r6ACGypqLNq"));
var app = builder.Build();

app.MapPost("/send-email", (MailMessage emailMessage, IEmailSender emailSender) =>
{
    emailMessage = new EmailMessageBuilder()
        .SetFrom("alenchaeto@mail.ru")
        .AddToRecipient("alenchaeto@mail.ru")
        .SetSubject("SubjectÎáúåêò")
        .SetBody("BodyÒåëî")
        .Build();

    emailSender.SendEmail(emailMessage);
    return Results.Ok();
});

app.Run();