namespace Elenktis.Azure
{
    public sealed class AzureManager : IAzure
    {
        public AzureManager()
        {
        }

        public IAzure AuthAndCreateInstance(string tenantId, string clientId, string clientSecret)
        {
            var azCred = new AzSDKCredentials(tenantId, clientId, clientSecret);

            return new AzureManager()
            {
                SubscriptionManager = new SubscriptionManager(azCred)
            };
        }

        public ISubscriptionManager SubscriptionManager { get; set; }
    }
}