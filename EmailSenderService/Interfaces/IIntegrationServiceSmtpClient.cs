using System.Net.Mail;

namespace EmailSenderService.Interfaces;

public interface IIntegrationServiceSmtpClient
{
    SmtpClient Client { get; }
}
