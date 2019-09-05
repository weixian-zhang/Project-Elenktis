using System;

namespace Elenktis.MessageBus
{
    public interface IMsgBusReceiver
    {
        T Receive<T>(string receiveFromQueueName);
    }
}
