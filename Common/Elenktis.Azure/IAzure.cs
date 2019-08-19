namespace Elenktis.Azure
{
    public interface IAzure
    {
        ISubscriptionManager SubscriptionManager { get; set; }

        IAzure AuthAndCreateInstance(string tenantId, string clientId, string clientSecret);
    }
}