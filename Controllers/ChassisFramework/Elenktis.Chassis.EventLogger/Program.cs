using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.MessageBus;
using Elenktis.Secret;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using NServiceBus;

namespace Elenktis.Chassis.EventLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try{
                await Init();
                    
                var hostBuilder = new HostBuilder();
                hostBuilder.ConfigureServices((context, services) => {

                    services.AddHostedService<MessageBusListenerService>(sp => {
                        return new MessageBusListenerService();
                    });
                    
                });
            }
            catch(Exception ex)
            {
                
            }
        }

        private static async Task Init()
        {
            try{
                _secrets = SecretHydratorFactory.Create().Hydrate<EventLoggerSecret>();

                var logDb = InitMongoDb();

                var eventBusConfig = ASBConfigFactory.Create
                    (QueueDirectory.EventLogger.DSSagaEvent,
                    _secrets.ServiceBusConnectionString, ControllerUri.EventLogger);

                eventBusConfig.RegisterComponents(config => {
                    config.ConfigureComponent<IMongoDatabase>(compFac => {
                        return logDb;
                    }, DependencyLifecycle.SingleInstance);

                    config.ConfigureComponent<LogStrategist>(compFac => {
                        return new LogStrategist(logDb);
                    },DependencyLifecycle.SingleInstance);
                });
                
                var errorBusConfig = ASBConfigFactory.Create
                    (QueueDirectory.EventLogger.Error,
                    _secrets.ServiceBusConnectionString, ControllerUri.EventLogger);

                await Endpoint.Start(eventBusConfig);

                await Endpoint.Start(errorBusConfig);
            }
            catch(Exception ex)
            {

            }
        }

        private static IMongoDatabase InitMongoDb()
        {
             var mongoClientSettings = new MongoClientSettings()
            {
                Server = new MongoServerAddress(_secrets.MongoHost, 10255),
                Credential =
                    MongoCredential.CreateCredential
                        ("pccore-eventlogger-dev-mongo",
                         _secrets.MongoUsername,
                         _secrets.MongoPassword),
                UseTls = true,
                ReplicaSetName = "globaldb"
            };

            string connectionString = _secrets.EventLoggerCosmosMongoDBConnectionString;
            MongoClientSettings settings =
                MongoClientSettings.FromUrl(new MongoUrl(connectionString));

            settings.SslSettings = 
            new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(mongoClientSettings);

            return mongoClient.GetDatabase("pccore-eventlogger-dev-mongo");
        }

        private static EventLoggerSecret _secrets;
    }
}
