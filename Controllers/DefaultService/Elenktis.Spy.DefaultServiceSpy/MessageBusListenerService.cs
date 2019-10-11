using System.Threading;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.Policy;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class MessageBusListenerService : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}