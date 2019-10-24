using Microsoft.Azure.Management.Security;

namespace Elenktis.Azure
{
    public interface IAzure
    {
        void SetCurrentSubscriptionId(string subscriptionId);

        ISubscriptionManager SubscriptionManager { get; set; }

        ISecurityCenterClient SecurityCenterClient { get; set; }
    }
}