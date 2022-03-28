using Microsoft.Extensions.Configuration;
using MicrosoftGraphExamples.Graph;
using System;
using System.Threading.Tasks;

namespace MicrosoftGraphExamples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = GraphServiceClientFactory.Create();
            
            var users = await client.Users.Request()
                //.Select(u => new
                //{
                //    u.Id,
                //    u.DisplayName,
                //    u.Messages,
                //    u.JobTitle
                //})
                .GetAsync();


            foreach(var user in users)
            {
                Console.WriteLine($"[{user.Id}] {user.DisplayName}");

                // Get email messages
                //var messages = await client.Users[user.Id].Messages.Request().GetAsync();

                // Url for email messages
                // Mail.Read
                // Mail.ReadWrite for moving messages about
                Console.WriteLine($" - messages: {client.Users[user.Id].Messages.RequestUrl}");
                Console.WriteLine($" - message attachments: {client.Users[user.Id].Messages["ABC123"].Attachments.RequestUrl}");
            }
        }
    }
}
