using System;
using System.Threading;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.MessageBus;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Saga.DefaultServiceSaga
{
    public class TimerService : BackgroundService
    {
        public TimerService(IEndpointInstance bus, IAzure azure)
        {
            _bus = bus;
            _azure = azure;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new Timer
                (StartSaga, null, TimeSpan.Zero, TimeSpan.FromSeconds(240));

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async void StartSaga(object state)
        {
            var subscriptions = await _azure.SubscriptionManager.GetAllSubscriptionsAsync();

            foreach (var sub in subscriptions)
            {
                await _bus.Send(QueueDirectory.Saga.DefaultService,
                    new AssessASCAutoRegisterVM() {
                        CorrelationId = MessageExtension.GenNewCorrelationId(),
                        SubscriptionId = sub.SubscriptionId
                });
            }
        }

        private Timer _timer;
        private IEndpointInstance _bus;
        private IAzure _azure;
    }
}