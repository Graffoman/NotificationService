using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NotificationService.Classes;
using NotificationService.Interfaces;
using NotificationService.Configuration;
using Microsoft.Extensions.Options;

namespace NetConsoleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Notification Service API",
                    Version = "v1"
                });
            });

            services.Configure<MailSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            services.Configure<RabbitSettings>(Configuration.GetSection("RabbitMQ"));
            services.AddSingleton<IHostedService, RabbitMQConsumer>(provider =>
            {
                var emailSender = provider.GetRequiredService<IEmailSender>();
                var rabbitSettings = provider.GetRequiredService<IOptions<RabbitSettings>>().Value;
                var logger = provider.GetRequiredService<ILogger<RabbitMQConsumer>>();
                return new RabbitMQConsumer(emailSender, rabbitSettings.HostName, rabbitSettings.UserName, rabbitSettings.Password, rabbitSettings.QueueName, logger);
            });

            services.AddLogging(configure => configure.AddConsole());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Service API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
