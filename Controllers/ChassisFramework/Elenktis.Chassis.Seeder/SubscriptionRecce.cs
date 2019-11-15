using System;

using Elenktis.Azure;
using Elenktis.Policy;
using Elenktis.Secret;
using System.Threading.Tasks;
using Elenktis.Policy.DefaultService;
using System.Reflection;

namespace Elenktis.Chassis.Seeder
{
    public class SubscriptionRecce : ISubscriptionRecce
    {
        public SubscriptionRecce
            (IAzure azure, IPlanCreationManager planManager, SeederSecret secrets) 
        {
            _planManager = planManager;
            _secrets = secrets;
        }
            
    
        public async Task StartAsync()
        {
            Init();

            var subscriptions = await _azure.SubscriptionManager.GetAllSubscriptionsAsync();

            foreach(var sub in subscriptions)
            {
                await _planManager.CreateDefaultServicePlansAsync(sub.SubscriptionId, false);

                await _planManager.CreateSecurityHygienePlanAsync(sub.SubscriptionId, false);

                await _planManager.CreateLogEnablerPlanAsync(sub.SubscriptionId, false);
            }
        }

        private void Init()
        {
            _azure = AzureRMFactory.AuthAndCreateInstance
                (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);
        }

        private IAzure _azure;

        private IPlanCreationManager _planManager;
        private ISecretHydrator _secretHydrator;
        private SeederSecret _secrets;
    }
}