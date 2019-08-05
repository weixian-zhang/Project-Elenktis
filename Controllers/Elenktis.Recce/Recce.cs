using System;
using System.Threading.Tasks;
using Elenktis.Assessment;
using Elenktis.Azure;
using Elenktis.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Elenktis.Recce
{
    public class Recce
    {
        public Recce(ISubscriptionRecce subscriptionRecce, IAzure azure,
            IPlanManager planManager, ISecretHydrator secretHydrator)
        {
            _subscriptionRecce = subscriptionRecce;
            _azure = azure;
            _planManager = planManager;
            _secretHydrator = secretHydrator;
        }

        [FunctionName("Recce")]
        public async Task Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            
        }

        private ISubscriptionRecce _subscriptionRecce;
        private IAzure _azure;
        private IPlanManager _planManager;
        private ISecretHydrator _secretHydrator;
        private ControllerSecret _secrets;
    }
}
