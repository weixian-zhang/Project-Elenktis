using System;
using Elenktis.Azure;
using Elenktis.MessageBus;
using Elenktis.Policy;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Elenktis.Fixer.DefaultService
{
    public class DefaultServiceFixer
    {
        public DefaultServiceFixer
            (IAzure azure, IPlanQueryManager planQueryManager)
        {

        }

        [FunctionName("DefaultServiceFixer")]
        public static void Run
            ([TimerTrigger("*/5 * * * * *", RunOnStartup =true, UseMonitor =true)]TimerInfo timerInfo, Microsoft.Extensions.Logging.ILogger log)
        {
            
        }

        private IAzure _azure;
        private IPlanQueryManager _planManager;
    }
}
