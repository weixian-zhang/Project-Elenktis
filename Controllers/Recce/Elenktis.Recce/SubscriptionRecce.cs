using System;

using Elenktis.Azure;
using Elenktis.Assessment;
using Elenktis.Configuration;
using System.Threading.Tasks;
using Elenktis.Assessment.DefaultService;

namespace Elenktis.Recce
{
    public class SubscriptionRecce : ISubscriptionRecce
    {
        public SubscriptionRecce
            (IAzure azure, IPlanCreationManager planManager) 
        {
            _azure = azure;
            _planManager = planManager;
            _secretHydrator = SecretHydratorFactory.Create();
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
            _secrets = _secretHydrator.Hydrate<ControllerSecret>();
            
            _azure = _azure.AuthAndCreateInstance
                (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);
        }

        private IAzure _azure;

        private IPlanCreationManager _planManager;
        private ISecretHydrator _secretHydrator;
        private ControllerSecret _secrets;
    }
}