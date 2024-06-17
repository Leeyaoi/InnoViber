using InnoViber.BLL.Interfaces;
using InnoViber.Domain.Providers;
using Microsoft.Extensions.Hosting;

namespace InnoViber.BLL.Services;

public class CheckIsSeenMessagesService : BackgroundService
{
    private readonly TimeSpan _period;
    private readonly IMessageService _messageService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CheckIsSeenMessagesService(IMessageService messageService, IDateTimeProvider timeProvider)
    {
        _period = TimeSpan.FromMinutes(20);
        _messageService = messageService;
        _dateTimeProvider = timeProvider;
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
                    continue; //this is for now
                }
            }
        }
    }
}
