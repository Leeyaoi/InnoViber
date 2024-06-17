using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace EmailSenderService
{
    public class EmailSender
    {
        private readonly string _appEmail;
        private readonly string _appName;
        private readonly string _userEmail;
        private readonly string _userName;
        private readonly SmtpClient _client;

        public EmailSender(string userName, string userEmail, IConfiguration config, IIntegrationServiceSmtpClient smtpClient)
        {
            _appEmail = config["EmailCredentials:Address"]!;
            _appName = config["EmailCredentials:Name"]!;
            _userEmail = userEmail;
            _userName = userName;
            _client = smtpClient.Client;
        }

        private MailMessage BuildMessage()
        {
            MailAddress from = new MailAddress(_appEmail, _appName);
            MailAddress to = new MailAddress(_userEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = $"Тест для {_userName}";
            m.Body = "Письмо-тест 2 работы smtp-клиента";
            return m;
        }

        public async Task SendEmailAsync()
        {
            var message = BuildMessage();
            await _client.SendMailAsync(message);
            Console.WriteLine("Письмо отправлено");
        }
    }
}
