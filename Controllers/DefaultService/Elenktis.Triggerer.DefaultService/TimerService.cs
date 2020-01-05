using System;
using System.Threading;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message.DefaultService;
using Elenktis.MessageBus;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Elenktis.Saga.DefaultServiceSaga
{
    public class TimerService : BackgroundService
    {
        public TimerService(IBusControl bus, IAzure azure, TriggererSecret secret)
        {
            _bus = bus;
            _azure = azure;
            _secret = secret;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new Timer
                (StartSaga, null, TimeSpan.Zero,
                 TimeSpan.FromMinutes
                    (Convert.ToDouble(10)));

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async void StartSaga(object state)
        {
            var subscriptions = await _azure.SubscriptionManager.GetAllSubscriptionsAsync();

            var uriBuilder = new UriBuilder(_bus.Address);
            uriBuilder.Path = QueueDirectory.Fixer.DefaultService;

            foreach (var sub in subscriptions)
            {
                var sendEndpoint = await _bus.GetSendEndpoint(uriBuilder.Uri);

                await sendEndpoint.Send(
                    new ASCAutoRegisterVMPolicyFix() {
                        CorrelationId = Guid.NewGuid(),
                        SubscriptionId = sub.SubscriptionId,
                        TimeSentToFixerFromTriggerer = DateTime.Now,
                        Controller = ControllerUri.DefaultServiceTriggerer
                        
                });
            }
        }

        private Timer _timer;
        private IBusControl _bus;
        private IAzure _azure;
        private TriggererSecret _secret;
    }
}