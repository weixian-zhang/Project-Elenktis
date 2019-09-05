namespace Elenktis.MessageBus
{
    public class AzSvcBusReceiver : IMsgBusReceiver
    {
        public T Receive<T>(string receiveFromQueueName)
        {

            return default(T);
        }
    }
}