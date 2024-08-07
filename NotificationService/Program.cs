using Microsoft.Extensions.DependencyInjection;
using NotificationService.Classes;
using NotificationService.Services;
using System.Net.Mail;
using NotificationService.Configuration;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IMailService, MailService>();



var smtpSettings = new MailSettings();
var configuration = builder.Configuration;
configuration.GetSection("SmtpSettings").Bind(smtpSettings);

//var smtpSettings = new SmtpSettings();
//var configuration = builder.Configuration;
//configuration.GetSection("SmtpSettings").Bind(smtpSettings);

var app = builder.Build();

var toList = new List<string>();
toList.Add("alenchaeto@mail.ru"); toList.Add("vsemposhapke@gmail.com");
var bccList = new List<string>();
var ccList = new List<string>();
var from = "alenchaeto@mail.ru";
var emailMessageItem = new MailData( " subject", "body ", from, "displayName",
                     "replyTo", "replyToName", bccList, ccList, toList);


var smtpSender = new SmtpEmailSender(smtpSettings.Host, smtpSettings.Port, smtpSettings.UserName, smtpSettings.Password);

app.MapPost("/sendEmail", () =>
{
    var emailBuilder = new EmailMessageBuilder(emailMessageItem);
    emailBuilder.SetFrom(emailMessageItem.From)
        .AddToRecipient(emailMessageItem.To)
        .SetSubject(emailMessageItem.Subject)
        .SetBody(emailMessageItem.Body)
        .Build();
});

//smtpSender.SendEmail(emailMessageItem.ToMailMessage());


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



