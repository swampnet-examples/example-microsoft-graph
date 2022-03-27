using Microsoft.Extensions.Configuration;
using O365Api.Graph;
using System;
using System.Threading.Tasks;

namespace O365Api
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cfg = LoadAppSettings();
            var client = GraphHelper.GetGraphServiceClient(cfg);
            
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

                //var u = await client.Users[user.Id].Request().GetAsync();

                //Console.WriteLine($"[{u.Id}]");
            }
        }


        static IConfigurationRoot LoadAppSettings()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
        }
    }
}
