using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Elenktis.MessageBus
{
    public class ASBConfigFactory
    {
        public static EndpointConfiguration Create(string queueName, string msgBusConnString)
        {
            var endpointConfiguration = new EndpointConfiguration(queueName);
            endpointConfiguration.SendFailedMessagesTo(QueueDirectory.EventLogger.Error);
            endpointConfiguration.AuditProcessedMessagesTo(QueueDirectory.EventLogger.MessageAudit);
            endpointConfiguration.EnableInstallers();
            
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(msgBusConnString);

            return endpointConfiguration;
        }

        public static EndpointConfiguration Create
            (string queueName, string msgBusConnString,
             out RoutingSettings<AzureServiceBusTransport> routes)
        {
            var endpointConfiguration = new EndpointConfiguration(queueName);
            endpointConfiguration.SendFailedMessagesTo(QueueDirectory.EventLogger.Error);
            endpointConfiguration.AuditProcessedMessagesTo(QueueDirectory.EventLogger.MessageAudit);
            endpointConfiguration.EnableInstallers();
            
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(msgBusConnString);

            routes = transport.Routing();

            return endpointConfiguration;
        }
    }
}
