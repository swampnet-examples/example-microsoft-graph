using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace MicrosoftGraphExamples.Graph
{
    public class GraphServiceClientFactory
    {
        private static IConfigurationRoot _cfg = null;

        public static GraphServiceClient Create()
        {
            // The client credentials flow requires that you request the
            // /.default scope, and preconfigure your permissions on the
            // app registration in Azure. An administrator must grant consent
            // to those permissions beforehand.
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            return new GraphServiceClient(
                GetTokenCredential(), 
                scopes);
        }


        private static TokenCredential GetTokenCredential()
        {
            _cfg ??= LoadAppSettings();

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            //var x = new DefaultAzureCredential()

            return new ClientSecretCredential(
                _cfg["tenantId"],
                _cfg["clientId"],
                _cfg["clientSecret"],
                options);
        }


        private static IConfigurationRoot LoadAppSettings()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
        }
    }
}
