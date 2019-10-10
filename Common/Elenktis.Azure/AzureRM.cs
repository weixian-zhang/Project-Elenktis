using Microsoft.Azure.Management.Security;

namespace Elenktis.Azure
{
    public sealed class AzureRM : IAzure
    {
        public ISubscriptionManager SubscriptionManager { get; set;}

        public ISecurityCenterClient SecurityCenterClient { get; set;}

        public void SetSubscriptionId(string subscriptionId)
        {
            _currentSubscriptionId = subscriptionId;

            SecurityCenterClient.SubscriptionId = _currentSubscriptionId;
        }

        private string _currentSubscriptionId = string.Empty;
    }
}