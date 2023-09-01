namespace SimpleBank.API.Services
{
    public class LocalMailService : IMailService
    {

        private readonly string from = "info@sbank.com";
        private readonly string to = "abdizamedmo@gmail.com";


        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {from} and to {to}, " + $"with {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Message : {message}");
        }

    }
}

