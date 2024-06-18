using AutoMapper;
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
    private readonly IMapper _mapper;

    public CheckIsSeenMessagesService(IServiceScopeFactory serviceScopeFactory, IDateTimeProvider timeProvider, IMapper mapper)
    {
        _period = TimeSpan.FromMinutes(20);
        _serviceScopeFactory = serviceScopeFactory;
        _dateTimeProvider = timeProvider;
        _mapper = mapper;
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
                if(!message.IsSeen && howLong > 20)
                {
                    var users = await GetUsers(message);
                    foreach (var user in users)
                    {
                        Publish(user);
                    }
                }
            }
        }
    }

    private string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }

    private void Publish(UserModel user)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var qName = "sendEmail";

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "sendEmail", durable: true, exclusive: false, autoDelete: true);

                var args = GetMessage([user.Name, user.Email]);

                var body = Encoding.UTF8.GetBytes(args);

                var prop = channel.CreateBasicProperties();
                prop.Persistent = true;

                channel.BasicPublish("", routingKey: qName, prop, body);
            }
        }
    }

    private async Task<List<UserModel>> GetUsers(MessageModel message)
    {
        if(message.Chat == null || message.Chat.Users == null)
        {
            return new List<UserModel>();
        }

        var users = _mapper.Map<List<UserModel>>(message.Chat.Users);

        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            var user = await userService.GetById(message.Chat.OwnerId, default);

            users.Add(user);
        }

        users.RemoveAll(x => x.Id == message.UserId);

        return users;
    }
}
