using System.Net.Mail;

namespace EmailSenderService;

public interface IIntegrationServiceSmtpClient
{
    SmtpClient Client { get; }
}
