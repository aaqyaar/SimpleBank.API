namespace SimpleBank.API.Services
{
    public class LocalMailService : IMailService
    {

        private readonly string from = string.Empty;
        private readonly string to = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            from = configuration["mailSettings:fromAddress"];
            to = configuration["mailSettings:toAddress"];
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {from} and to {to}, " + $"with {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Message : {message}");
        }

    }
}

