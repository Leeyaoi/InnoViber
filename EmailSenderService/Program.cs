using EmailSenderService.Interfaces;
using EmailSenderService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Text;
using System.Threading.Channels;

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

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var qName = "sendEmail";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "sendEmail", durable: true, exclusive: false, autoDelete: true);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(qName, autoAck: false, consumer);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body).Split(" ");

                        var name = message[0];
                        var email = message[1];

                        try
                        {
                            sender.SendEmailAsync(name, email).GetAwaiter();
                        }
                        catch (SmtpException ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                        channel.BasicAck(ea.DeliveryTag, multiple: false);
                    };
                }
            }
            
            Console.Read();
        }
    }
}
