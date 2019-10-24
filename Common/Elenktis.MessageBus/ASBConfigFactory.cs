using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Elenktis.Message;
using Newtonsoft.Json;
using NServiceBus;
using NServiceBus.Persistence.Sql;

namespace Elenktis.MessageBus
{
    public class ASBConfigFactory
    {
        public static EndpointConfiguration Create
            (string queueName, string msgBusConnString, string controllerName)
        {
            var endpointConfiguration = new EndpointConfiguration(queueName);
            endpointConfiguration.SendFailedMessagesTo(QueueDirectory.EventLogger.Error);
            //endpointConfiguration.AuditProcessedMessagesTo(QueueDirectory.EventLogger.SagaAudit);
            endpointConfiguration.EnableInstallers();

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Failed(
                failed => {
                    failed.HeaderCustomization(headers => {
                        headers.Add("controller", controllerName);
                    });
            });

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var serialization = endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            serialization.Settings(jsonSettings);

            //endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(msgBusConnString);

            var routes = transport.Routing();
            //default routes for error and health
            routes.RouteToEndpoint(typeof(ErrorEvent), QueueDirectory.EventLogger.Error);
            routes.RouteToEndpoint(typeof(HealthEvent), QueueDirectory.EventLogger.ControllerHealth);
           
           return endpointConfiguration;
        }

        //create endpoint config with persistent saga storage
        public static EndpointConfiguration Create
            (string queueName, string msgBusConnString,
             string controllerName, string persistentConnString)
        {
            var endpointConfiguration = new EndpointConfiguration(queueName);
            endpointConfiguration.SendFailedMessagesTo(QueueDirectory.EventLogger.Error);
            //endpointConfiguration.AuditProcessedMessagesTo(QueueDirectory.EventLogger.SagaAudit);
            endpointConfiguration.EnableInstallers();

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Failed(
                failed => {
                    failed.HeaderCustomization(headers => {
                        headers.Add("controller", controllerName);
                    });
            });

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var serialization = endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            serialization.Settings(jsonSettings);

            var sagaPersistent = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = sagaPersistent.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            sagaPersistent.SqlDialect<SqlDialect.MsSqlServer>();
            sagaPersistent.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(persistentConnString);
                });
            
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(msgBusConnString);

            var routes = transport.Routing();
            //default routes for error and health
            routes.RouteToEndpoint(typeof(ErrorEvent), QueueDirectory.EventLogger.Error);
            routes.RouteToEndpoint(typeof(HealthEvent), QueueDirectory.EventLogger.ControllerHealth);
           
           return endpointConfiguration;
        }

        // public static EndpointConfiguration Create
        //     (string queueName, string msgBusConnString, string controllerName,
        //      out RoutingSettings<AzureServiceBusTransport> routes)
        // {
        //     var endpointConfiguration = new EndpointConfiguration(queueName);
        //     endpointConfiguration.SendFailedMessagesTo(QueueDirectory.EventLogger.Error);
        //     endpointConfiguration.AuditProcessedMessagesTo(QueueDirectory.EventLogger.SagaAudit);
        //     endpointConfiguration.EnableInstallers();

        //     var recoverability = endpointConfiguration.Recoverability();
        //     recoverability.Failed(settings => {
        //         settings.HeaderCustomization(headers => {
        //             headers.Add("controller", controllerName);
        //         });
        //     });

        //     var settings = new JsonSerializerSettings
        //     {
        //         Formatting = Formatting.Indented
        //     };
        //     var serialization = endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
        //     serialization.Settings(settings);
            
        //     var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        //     transport.ConnectionString(msgBusConnString);

        //     routes = transport.Routing();
        //     //default routes for error and health
        //     routes.RouteToEndpoint(typeof(ErrorEvent), QueueDirectory.EventLogger.Error);
        //     routes.RouteToEndpoint(typeof(HealthEvent), QueueDirectory.EventLogger.ControllerHealth);

        //     return endpointConfiguration;
        // }
    }
}
