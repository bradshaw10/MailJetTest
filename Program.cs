using System;
using Mailjet.Client;
using Mailjet.Client.Resources;
using System.Threading.Tasks;
using Mailjet.Client.TransactionalEmails;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace MailJetTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            MailjetClient client = new MailjetClient("07504f4dbf9d9e35d3b3362dfcd6791f",
                "7779f26f8e94259fabd64ddbdc5d6747");

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
                       .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "eoghan.bradshaw@gmail.com"},
                  {"Name", "Me"}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", "e.bradshaw@retail-int.com"},
                   {"Name", "You"}
                   }
                  }},
                 {"Subject", "My first Mailjet Email!"},
                 {"TextPart", "Greetings from Mailjet!"},
                 {"HTMLPart", "<h3>Dear passenger 1, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!</h3><br />May the delivery force be with you!"}
                 }
                           });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
