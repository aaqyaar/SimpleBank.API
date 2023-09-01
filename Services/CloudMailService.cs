namespace SimpleBank.API.Services
{
	public class CloudMailService: IMailService
	{
        private readonly string from = "info@sbank.com";
        private readonly string to = "abdizamedmo@gmail.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {from} and to {to}, " + $"with {nameof(CloudMailService)}. CLOud waye");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Message : {message}");
        }
    }
}

