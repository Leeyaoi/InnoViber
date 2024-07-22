using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.Domain.Providers;
using InnoViber.User.DAL.Interfaces;
using InnoViber.User.DAL.Models;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedModels;

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
                        if (user.Email != "")
                        {
                            await Publish(user, author, message.Chat.Name, howLong);
                        }
                    }
                }
            }
        }
    }

    private Task Publish(ExternalUserModel user, string author, string chatName, double howLong)
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
            var userService = scope.ServiceProvider.GetRequiredService<IUserHttpService>();

            var user = await userService.GetUser(message.UserId, default);

            return user!.Name;
        }
    }

    private async Task<List<ExternalUserModel>> GetUsers(MessageModel message)
    {
        if (message.Chat == null || message.Chat.Roles == null)
        {
            return new List<ExternalUserModel>();
        }

        var users = new List<ExternalUserModel>();

        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserHttpService>();

            foreach (var role in message.Chat.Roles)
            {
                var user = await userService.GetUser(role.UserId, default);
                if (user != null)
                {
                    users.Add(user);
                }
            }
        }

        users.RemoveAll(x => x.Auth0Id == message.UserId);

        return users;
    }
}