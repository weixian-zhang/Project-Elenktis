using System;
using System.Threading.Tasks;
using MassTransit;

namespace Elenktis.MessageBus
{
    public interface IMsgBusReceiver<TConsumer> where TConsumer : class, IConsumer, new()
    {
        void InitConsumer(string queueName);
    }
}
