using System;
using System.Threading.Tasks;
using Elenktis.Secret;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;

namespace Elenktis.MessageBus
{
    public class AzSvcBusSender : IMsgBusSender
    {
        public AzSvcBusSender(ISecretHydrator hydrator)
        {
            Init(hydrator);
        }

        public async Task SendAsync<T>(string queueName, T message)
        {
            //https://stackoverflow.com/questions/56483056/queue-not-found-when-using-masstransit-sendendpoint-to-send-a-message

            try
            {
                await _bus.StartAsync();

                ISendEndpoint endpoint=
                    await _bus.GetSendEndpoint(new Uri(_bus.Address, queueName));

                await endpoint.Send(message);
            }
            catch(Exception ex)
            {
                await _bus.StopAsync();
            }
            finally
            {
                await _bus.StopAsync();
            }
        }

        private void Init(ISecretHydrator hydrator)
        {
            _secret = hydrator.Hydrate<ControllerSecret>();

            _bus = Bus.Factory.CreateUsingAzureServiceBus(config => {
                config.Host(_secret.ServiceBusConnectionString, c => {});
            });   
        }

        private ControllerSecret _secret;
        private IBusControl _bus;
    }
}
