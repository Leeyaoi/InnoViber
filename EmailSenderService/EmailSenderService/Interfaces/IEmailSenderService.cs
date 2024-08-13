using SharedModels;

namespace EmailSenderService.Interfaces;

public interface IEmailSenderService
{
    Task SendEmailAsync(IUserInfo userInfo);
}
