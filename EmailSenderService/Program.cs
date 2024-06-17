using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;

namespace EmailSenderService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton<IIntegrationServiceSmtpClient, IntegrationServiceSmtpClient>();

            var host = builder.Build();

            var smtpClient = host.Services.GetRequiredService<SmtpClient>();
            var config = host.Services.GetRequiredService<IConfiguration>();

            var sender = new EmailSender("Darya", "work.yaskodarya@gmail.com", config, smtpClient);
            try
            {
                sender.SendEmailAsync().GetAwaiter();
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Read();
        }
    }
}
