using System.Threading;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.Policy;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class ScheduledBasedBackgroundService : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }
    }
}