using System.Threading.Tasks;
using Elenktis.Secret;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;

namespace Elenktis.MessageBus
{
    public class AzSvcBusReceiver<TConsumer> : IMsgBusReceiver<TConsumer>
    where TConsumer : class, IConsumer, new()
    {
        public AzSvcBusReceiver(ISecretHydrator secretHydrator)
        {
            Init(secretHydrator);
        }

        public void InitConsumer(string queueName)
        {
            var busControl = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
            {
                IServiceBusHost host = cfg.Host(_secret.ServiceBusConnectionString, c => {});

                cfg.ReceiveEndpoint(host, queueName, e =>
                {
                    e.Consumer<TConsumer>();
                });
            });
        }

        private void Init(ISecretHydrator secretHydrator)
        {
            _secret = secretHydrator.Hydrate<ControllerSecret>();
        }

        private ControllerSecret _secret;
        private IBusControl _bus;
    }
}