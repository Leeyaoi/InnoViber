using EmailSenderService.Interfaces;
using EmailSenderService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;

namespace EmailSenderService
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton<IIntegrationServiceSmtpClient, IntegrationServiceSmtpClient>();
            builder.Services.AddSingleton<IEmailSenderService, EmailSender>();

            var host = builder.Build();

            var sender = host.Services.GetRequiredService<IEmailSenderService>();

            try
            {
                sender.SendEmailAsync("Darya", "work.yaskodarya@gmail.com").GetAwaiter();
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Read();
        }
    }
}
