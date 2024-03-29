using System.Threading;
using System.Threading.Tasks;
using Elenktis.Message;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Chassis.EventLogger
{
    public class MessageBusListenerService : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {   
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}