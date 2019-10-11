using System;
using System.Threading.Tasks;
using Elenktis.Message;
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

            var routes = transport.Routing();
            //default routes for error and health
            routes.RouteToEndpoint(typeof(ErrorEvent), QueueDirectory.EventLogger.Error);
            routes.RouteToEndpoint(typeof(HealthEvent), QueueDirectory.EventLogger.ControllerHealth);
           
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
            //default routes for error and health
            routes.RouteToEndpoint(typeof(ErrorEvent), QueueDirectory.EventLogger.Error);
            routes.RouteToEndpoint(typeof(HealthEvent), QueueDirectory.EventLogger.ControllerHealth);

            return endpointConfiguration;
        }
    }
}
