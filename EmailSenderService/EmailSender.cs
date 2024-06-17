using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace EmailSenderService
{
    public class EmailSender
    {
        private readonly string _appEmail;
        private readonly string _appPassword;
        private readonly string _appName;
        private readonly string _userEmail;
        private readonly string _userName;

        public EmailSender(string userName, string userEmail)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();
            _appEmail = config["EmailCredentials:Address"];
            _appPassword = config["EmailCredentials:Password"];
            _appName = config["EmailCredentials:Name"];
            _userEmail = userEmail;
            _userName = userName;
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

        private SmtpClient BuildClient()
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(_appEmail, _appPassword);
            smtp.EnableSsl = true;
            return smtp;
        }

        public async Task SendEmailAsync()
        {
            var message = BuildMessage();
            var smtp = BuildClient();
            await smtp.SendMailAsync(message);
            Console.WriteLine("Письмо отправлено");
        }
    }
}
