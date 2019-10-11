using System;
using System.Threading;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.Policy;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class SubscriptionAssessorService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}