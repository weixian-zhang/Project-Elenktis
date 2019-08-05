using System;

using Elenktis.Azure;
using Elenktis.Assessment;
using Elenktis.Configuration;
using System.Threading.Tasks;

namespace Elenktis.Recce
{
    public class SubscriptionRecce : ISubscriptionRecce
    {
        public SubscriptionRecce
            (IAzure azure, IPlanManager planManager, ISecretHydrator secretHydrator) 
        {
            _azure = azure;
            _planManager = planManager;
            _secretHydrator = secretHydrator;
        }
            
    
        public async Task StartAsync()
        {
            var subscriptions = await _azure.SubscriptionManager.GetAllSubscriptionsAsync();
        }

        private IAzure _azure;
        private IPlanManager _planManager;
        private ISecretHydrator _secretHydrator;
        private ControllerSecret _secrets;
    }
}