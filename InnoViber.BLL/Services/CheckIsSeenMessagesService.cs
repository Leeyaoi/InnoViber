using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.Domain.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Text;
using System.Xml.Linq;

namespace InnoViber.BLL.Services;

public class CheckIsSeenMessagesService : BackgroundService
{
    private readonly TimeSpan _period;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CheckIsSeenMessagesService(IServiceScopeFactory serviceScopeFactory, IDateTimeProvider timeProvider)
    {
        _period = TimeSpan.FromMinutes(1);
        _serviceScopeFactory = serviceScopeFactory;
        _dateTimeProvider = timeProvider;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync())
        {
            List<MessageModel> messages;

            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();
                messages = await messageService.GetAll(stoppingToken);
            }

            foreach (var message in messages)
            {
                var howLong = (_dateTimeProvider.GetDate() - message.Date).TotalMinutes;
                if(!message.IsSeen() && howLong > 3)
                {
                    Publish(message);
                }
            }
        }
    }

    static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }

    private void Publish(MessageModel message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var qName = "sendEmail";

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "sendEmail", durable: true, exclusive: false, autoDelete: true);

                var args = GetMessage([message.User.Name, message.User.Email]);

                var body = Encoding.UTF8.GetBytes(args);

                var prop = channel.CreateBasicProperties();
                prop.Persistent = true;

                channel.BasicPublish("", routingKey: qName, prop, body);
            }
        }
    }
}
