using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Elenktis.MessageBus
{
    public interface IMsgBusConfigFactory
    {
        EndpointConfiguration Create(string queueName, string msgBusConnString);
    }
}
