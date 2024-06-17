using System.Net.Mail;

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
