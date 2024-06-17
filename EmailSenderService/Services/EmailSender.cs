using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using EmailSenderService.Interfaces;

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

        private MailMessage BuildMessage(string userName, string userEmail)
        {
            MailAddress from = new MailAddress(_appEmail, _appName);
            MailAddress to = new MailAddress(userEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = $"Тест для {userName}";
            m.Body = "Письмо-тест 2 работы smtp-клиента";
            return m;
        }

        public async Task SendEmailAsync(string userName, string userEmail)
        {
            var message = BuildMessage(userName, userEmail);
            await _client.SendMailAsync(message);
            Console.WriteLine("Письмо отправлено");
        }
    }
}
