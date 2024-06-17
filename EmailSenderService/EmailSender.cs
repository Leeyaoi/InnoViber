using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

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

        public async Task SendEmailAsync()
        {
            MailAddress from = new MailAddress(_appEmail, _appName);
            MailAddress to = new MailAddress(_userEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = $"Тест для {_userName}";
            m.Body = "Письмо-тест 2 работы smtp-клиента";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(_appEmail, _appPassword);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            Console.WriteLine("Письмо отправлено");
        }
    }
}
