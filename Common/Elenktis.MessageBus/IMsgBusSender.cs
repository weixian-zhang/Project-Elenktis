using System;
using System.Threading.Tasks;

namespace Elenktis.MessageBus
{
    public interface IMsgBusSender
    {
        Task SendAsync<T>(string queueName, T message);
    }
}
