using System;
using System.Threading.Tasks;
using Elenktis.Policy;
using Elenktis.Azure;
using Elenktis.Secret;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Elenktis.Recce
{
    public class Recce
    {
        public Recce(ISubscriptionRecce subRecce)
        {
            _subRecce = subRecce;
        } 

        [FunctionName("Recce")]
        public async Task Run([TimerTrigger("*/15 * * * * *")]TimerInfo myTimer, ILogger log)
        {
           await _subRecce.StartAsync();
        }

        private ISubscriptionRecce _subRecce;
    }
}
