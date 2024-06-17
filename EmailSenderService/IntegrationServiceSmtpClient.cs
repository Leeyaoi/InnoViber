using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace EmailSenderService;

public class IntegrationServiceSmtpClient : IIntegrationServiceSmtpClient
{
    public SmtpClient Client { get; }

    public IntegrationServiceSmtpClient(IConfiguration config)
    {
        Client = new SmtpClient()
        {
            Port = config.GetValue<int>("EmailCredentials:Port"),
            Host = config["EmailCredentials:Host"]!,
            Credentials = new NetworkCredential(config["EmailCredentials:Address"]!, config["EmailCredentials:Passkey"]!),
            EnableSsl = true
        };
    }
}
