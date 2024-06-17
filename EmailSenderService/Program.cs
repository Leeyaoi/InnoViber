using System.Net.Mail;
using System.Net;
using System;
using System.Threading.Tasks;

namespace EmailSenderService
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new EmailSender("Darya", "work.yaskodarya@gmail.com");
            try
            {
                sender.SendEmailAsync().GetAwaiter();
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Read();
        }
    }
}
