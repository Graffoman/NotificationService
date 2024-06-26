using NotificationService.Classes;
using NotificationService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Net.Mail;


var builder = WebApplication.CreateBuilder(args);
IEmailSender emailSender = new SmtpEmailSender("smtp.mail.ru", 587, "alenchaeto@mail.ru", "JMvwj6tD3r6ACGypqLNq");
var app = builder.Build();

app.MapPost("/sendEmail", () =>
{
    var emailMessage = new EmailMessageBuilder()
        .SetFrom("alenchaeto@mail.ru")
        .AddToRecipient("alenchaeto@mail.ru")
        .SetSubject("Subject2")
        .SetBody("Body2")
        .Build(); 
    
    emailSender.SendEmail(emailMessage.ToMailMessage());
    return Results.Ok();
});

app.Run();


//builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>(sp =>
//    new SmtpEmailSender("smtp.mail.ru", 587, "alenchaeto@mail.ru", "JMvwj6tD3r6ACGypqLNq"));

//app.MapPost("/sendEmail", (EmailMessage emailMessage, IEmailSender emailSender) =>
//{
//    emailMessage = new EmailMessageBuilder()
//        .SetFrom("alenchaeto@mail.ru")
//        .AddToRecipient("alenchaeto@mail.ru")
//        .SetSubject("Subject2")
//        .SetBody("Body2")
//        .Build();

//    emailSender.SendEmail(emailMessage.ToMailMessage());

//    return Results.Ok();
//});