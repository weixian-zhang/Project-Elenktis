using System;
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
            await Init();
            
           var hostBuilder = new HostBuilder();
           hostBuilder.ConfigureServices((context, services) => {
               services.AddHostedService<MessageBusListenerService>(sp => {
                   return new MessageBusListenerService();
               });
           });
        }

        private static async Task Init()
        {
            _secrets = SecretHydratorFactory.Create().Hydrate<EventLoggerSecret>();

            InitMongoDbs();

            var eventBusConfig = ASBConfigFactory.Create
                (QueueDirectory.EventLogger.AppEvent,
                 _secrets.ServiceBusConnectionString, ControllerUri.EventLogger);

            eventBusConfig.RegisterComponents(config => {
                config.ConfigureComponent<IMongoDatabase>(compFac => {
                    return _eventDb;
                }, DependencyLifecycle.SingleInstance);
            });
            
            var errorBusConfig = ASBConfigFactory.Create
                (QueueDirectory.EventLogger.Error,
                 _secrets.ServiceBusConnectionString, ControllerUri.EventLogger);

            errorBusConfig.RegisterComponents(config => {
                config.ConfigureComponent<IMongoDatabase>(compFac => {
                    return _eventDb;
                }, DependencyLifecycle.SingleInstance);
            });

            await Endpoint.Start(eventBusConfig);

            await Endpoint.Start(errorBusConfig);
        }

        private static void InitMongoDbs()
        {
            var dsSagaMC =
                new MongoClient(new MongoUrl(_secrets.EventLoggerCosmosMongoDBConnectionString));
            
            _eventDb = dsSagaMC.GetDatabase("pccore-eventlogger-dev-mongo");
        }

        private static EventLoggerSecret _secrets;
        private static IMongoDatabase _eventDb;
    }
}
