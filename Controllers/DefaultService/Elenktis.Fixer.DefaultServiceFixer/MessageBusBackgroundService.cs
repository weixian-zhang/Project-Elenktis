using System.Threading;
using System.Threading.Tasks;
using Elenktis.Message.DefaultService;
using Elenktis.Policy;
using Elenktis.Secret;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using Elenktis.Message;

namespace Elenktis.Fixer.DefaultServiceFixer
{
    public class MessageBusBackgroundService : BackgroundService
    {
        public MessageBusBackgroundService(IPlanQueryManager planQueryManager)
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }

        




        
        private IPlanQueryManager _planQueryManager;

        private DSFixerSecret _secrets;

    }

}