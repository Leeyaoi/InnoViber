using EmailSenderService.Interfaces;
using EmailSenderService.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmailSenderService
{
    public static class Program
    {
        static void Main(string[] args)
        {

            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton<IIntegrationServiceSmtpClient, IntegrationServiceSmtpClient>();
            builder.Services.AddSingleton<IEmailSenderService, EmailSender>();
            builder.Services.AddMassTransit(x =>
            {
                var assembly = typeof(Program).Assembly;
                x.AddConsumers(assembly);
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("UserInfoQueue", e =>
                    {
                        e.ConfigureConsumer<UserInfoConsumer>(context);
                    });
                });
            });

            builder.Build();

            Console.Read();
        }
    }
}
