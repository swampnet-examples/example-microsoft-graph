using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace MicrosoftGraphExamples.Graph
{
    public class GraphHelper
    {
        public static GraphServiceClient GetGraphServiceClient(IConfiguration cfg)
        {
            // The client credentials flow requires that you request the
            // /.default scope, and preconfigure your permissions on the
            // app registration in Azure. An administrator must grant consent
            // to those permissions beforehand.
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            return new GraphServiceClient(
                GetTokenCredential(cfg), 
                scopes);
        }

        private static TokenCredential GetTokenCredential(IConfiguration cfg)
        {
            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            //var x = new DefaultAzureCredential()

            return new ClientSecretCredential(
                cfg["tenant"],
                cfg["clientId"],
                cfg["clientSecret"],
                options);
        }
    }
}
