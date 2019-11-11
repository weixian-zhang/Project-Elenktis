
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Elenktis.Chassis.Seeder
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
