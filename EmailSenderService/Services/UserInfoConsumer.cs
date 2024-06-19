using EmailSenderService.Interfaces;
using MassTransit;
using SharedModels;

namespace EmailSenderService.Services;

public class UserInfoConsumer : IConsumer<IUserInfo>
{
    private readonly IEmailSenderService _sender;

    public UserInfoConsumer(IEmailSenderService senderService)
    {
        _sender = senderService;
    }

    public Task Consume(ConsumeContext<IUserInfo> context)
    {
        var userInfo = context.Message;
        return _sender.SendEmailAsync(userInfo);
    }
}
