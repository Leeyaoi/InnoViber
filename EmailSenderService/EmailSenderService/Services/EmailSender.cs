using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using EmailSenderService.Interfaces;
using SharedModels;

namespace EmailSenderService.Services
{
    public class EmailSender : IEmailSenderService
    {
        private readonly string _appEmail;
        private readonly string _appName;
        private readonly SmtpClient _client;

        public EmailSender(IConfiguration config, IIntegrationServiceSmtpClient smtpClient)
        {
            _appEmail = config["EmailCredentials:Address"]!;
            _appName = config["EmailCredentials:Name"]!;
            _client = smtpClient.Client;
        }

        private MailMessage BuildMessage(IUserInfo userInfo)
        {
            MailAddress from = new MailAddress(_appEmail, _appName);
            MailAddress to = new MailAddress(userInfo.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = $"Dear {userInfo.UserName}";
            m.Body = $"{userInfo.AuthorName} is waiting for your responce in {userInfo.ChatName} " +
                $"chat for {userInfo.HowLong} minutes";
            return m;
        }

        public async Task SendEmailAsync(IUserInfo userInfo)
        {
            var message = BuildMessage(userInfo);
            await _client.SendMailAsync(message);
            Console.WriteLine("Send");
        }
    }
}
