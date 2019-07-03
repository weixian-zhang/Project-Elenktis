using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elenktis.Azure
{
    public class SubscriptionManager : ISubscriptionManager
    {
        public SubscriptionManager(AzSDKCredentials sdkCred)
        {
            _sdkCreds = sdkCred;
        }

        public async Task<IEnumerable<TenantSubscription>> GetAllSubscriptionsAsync()
        {
            ISubscriptionClient subClient = new SubscriptionClient(_sdkCreds);
            
            IEnumerable<Subscription> subscriptions = await subClient.Subscriptions.ListAsync();

            IEnumerable<TenantSubscription> tSubs = from s in subscriptions
                                                    where s.DisplayName.ToLowerInvariant() != "centralservices"
                                                     select new TenantSubscription
                                                     {
                                                         SubscriptionId = s.SubscriptionId,
                                                         DisplayName = s.DisplayName
                                                     };

            foreach (var sub in tSubs)
            {
                var rmClient = new ResourceManagementClient(_sdkCreds);
                rmClient.SubscriptionId = sub.SubscriptionId;
                var rgs = await rmClient.ResourceGroups.ListAsync();
                sub.ResourceGroups = rgs;
            }

            return tSubs;
        }

        private AzSDKCredentials _sdkCreds;
    }
}
