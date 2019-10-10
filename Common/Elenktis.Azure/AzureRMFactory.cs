using Microsoft.Azure.Management.Security;

namespace Elenktis.Azure
{
    public sealed class AzureRMFactory
    {
        public AzureRMFactory()
        {
        }

        public static IAzure AuthAndCreateInstance(string tenantId, string clientId, string clientSecret)
        {
            _azCred = new AzSDKCredentials(tenantId, clientId, clientSecret);

            return new AzureRM()
            {
                SubscriptionManager = new SubscriptionManager(_azCred),
                SecurityCenterClient = new SecurityCenterClient(_azCred)
            };
        }

        private static AzSDKCredentials _azCred;
    }
}