namespace EmailSenderService.Interfaces;

public interface IEmailSenderService
{
    Task SendEmailAsync(string userName, string userEmail);
}
