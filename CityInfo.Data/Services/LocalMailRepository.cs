using CityInfo.Data.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace CityInfo.Data.Services
{
    public class LocalMailRepository : IMailRepository
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailRepository(IConfiguration configuration)
        {
            _mailTo = configuration["MailSettings:MailToAddress"];
            _mailFrom = configuration["MailSettings:MailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            // send mail - output to console window
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(LocalMailRepository)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
