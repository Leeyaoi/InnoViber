using InnoViber.BLL.Interfaces;
using InnoViber.Domain.Providers;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Text;

namespace InnoViber.BLL.Services;

public class CheckIsSeenMessagesService : BackgroundService
{
    private readonly TimeSpan _period;
    private readonly IMessageService _messageService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IModel _channel;

    public CheckIsSeenMessagesService(IMessageService messageService, IDateTimeProvider timeProvider)
    {
        _period = TimeSpan.FromMinutes(20);
        _messageService = messageService;
        _dateTimeProvider = timeProvider;

        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: "sendEmail", durable: true, exclusive: false, autoDelete: true);

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync())
        {
            var messages = await _messageService.GetAll(stoppingToken);
            foreach (var message in messages)
            {
                var howLong = (message.Date - _dateTimeProvider.GetDate()).TotalMinutes;
                if(!message.IsSeen() && howLong > 30)
                {
                    var args = GetMessage([message.User.Name, message.User.Email]);
                    var body = Encoding.UTF8.GetBytes(args);
                    _channel.BasicPublish(exchange: string.Empty, routingKey: "sendEmail", basicProperties: null, body: body);
                }
            }
        }
    }

    static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }
}
