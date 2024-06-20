using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.Domain.Providers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using SharedModels;
using System.Text;
using System.Threading.Tasks.Sources;

namespace InnoViber.BLL.Services;

public class CheckIsSeenMessagesService : BackgroundService
{
    private readonly TimeSpan _period;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CheckIsSeenMessagesService(IServiceScopeFactory serviceScopeFactory, IDateTimeProvider timeProvider)
    {
        _period = TimeSpan.FromMinutes(20);
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
                var author = await GetAuthorName(message);
                if (!message.IsSeen && howLong > 20)
                {
                    var users = await GetUsers(message);
                    foreach (var user in users)
                    {
                        await Publish(user, author, message.Chat.Name, howLong);
                    }
                }
            }
        }
    }

    private Task Publish(UserModel user, string author, string chatName, double howLong)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            return publishEndpoint.Publish<IUserInfo>(new
            {
                UserName = user.Name,
                user.Email,
                HowLong = howLong,
                AutorName = author,
                ChatName = chatName
            });
        }
    }

    private async Task<string> GetAuthorName(MessageModel message)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            var user = await userService.GetById(message.UserId, default);

            return user!.Name;
        }
    }

    private async Task<List<UserModel>> GetUsers(MessageModel message)
    {
        if(message.Chat == null || message.Chat.Roles == null)
        {
            return new List<UserModel>();
        }

        var users = new List<UserModel>();

        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            foreach (var role in message.Chat.Roles)
            {
                var user = await userService.GetById(role.UserId, default);
                if(user != null)
                {
                    users.Add(user);
                }
            }
        }

        users.RemoveAll(x => x.Id == message.UserId);

        return users;
    }
}
